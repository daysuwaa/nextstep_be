using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using nextstep.application.Abstractions.Core; 
using nextstep.application.DTOs.Responses;
using nextstep.infrastructure;
using nextstep.Models.Requests;
using nextstep.application.Handlers;
using System;
using System.Net;
using System.Linq;
using nextstep.API.Helpers;

namespace nextstep.Endpoints.AuthenticationEndpoints
{
    public static class AuthenticationModule
    {
        public static void AddAuthenticationEndpoint(this IEndpointRouteBuilder app)
        {
            // ========================= LOGIN ENDPOINT =========================
            app.MapPost("api/v1/login", async ([FromBody] LoginReqModel loginRequest, IValidator<LoginReqModel> validator, IUserHandler userHandler) =>
            {
                // this validates the login request
                var validationResult = await validator.ValidateAsync(loginRequest);
                // if the validationresult is NOT valid, then return a bad requet
                if (!validationResult.IsValid)
                    return Results.BadRequest(
                        ApiResponse<object>.ErrorResponse(validationResult.Errors.First().ErrorMessage)
                    );

                try
                {
                    //var tokenResult = await userHandler.LoginAsync(loginRequest.Email, loginRequest.Password);

                    //// Return a 200 OK response with a success message and token data
                    //return Results.Ok(ApiResponse<object>.SuccessResponse(
                    //    "Login successful",
                    //    new { Token = tokenResult }
                    //));


                    var tokenResult = await userHandler.LoginAsync(loginRequest.Email, loginRequest.Password);

                    // Include UserId in the response
                    return Results.Ok(ApiResponse<object>.SuccessResponse(
                        "Login successful",
                        new
                        {
                            Token = tokenResult.Token,
                            TokenExpiry = tokenResult.TokenExpiry,
                            UserId = tokenResult.UserId
                        }
                    ));
                }

                // this blocks the run- if invalid, yfm
                catch (UnauthorizedAccessException)
                {
                    // the returns a bad requesy
                    return Results.BadRequest(
                        ApiResponse<object>.ErrorResponse("User is unauthorized")
                    );
                }
                catch (Exception ex)
                {
                    return Results.Problem(
                        detail: ex.Message,
                        title: "An unexpected error occurred",
                        statusCode: 500
                    );
                }
            });

            // ========================= REGISTER ENDPOINT =========================
            app.MapPost("api/v1/register", async ([FromBody] RegisterReqModel request, IValidator<RegisterReqModel> validator, IUserHandler userHandler) =>
            {
                var validationResult = await validator.ValidateAsync(request);
                if (!validationResult.IsValid)
                    return Results.BadRequest(
                        ApiResponse<object>.ErrorResponse(validationResult.Errors.First().ErrorMessage)
                    );

                try
                {
                    // this calls the register async method
                    var newUser = await userHandler.RegisterAsync(request.Username, request.Email, request.Password);

                    return Results.Ok(ApiResponse<object>.SuccessResponse("Registration successful",
                            new { newUser.id, newUser.email, newUser.name }
                        )
                    );
                }
                catch (InvalidOperationException ex)
                {
                    return Results.BadRequest(
                        ApiResponse<object>.ErrorResponse(ex.Message)
                    );
                }
                catch (Exception ex)
                {
                    return Results.Problem(
                        detail: ex.Message,
                        title: "An unexpected error occurred",
                        statusCode: 500
                    );
                }
            });
            //            app.MapGet("api/v1/me", async (HttpContext context, AppDbContext db) =>
            //            {
            //                var userId = UserHelper.GetUserIdFromContext(context);

            //                if (int.Parse(userId))
            //                    return Results.BadRequest(
            //                          ApiResponse<object>.ErrorResponse("Unauthorized")
            //                      );

            //                var user = await db.Users.FindAsync(int.Parse(userId));
            //                if (user == null)
            //                    return Results.NotFound(new { message = "User not found" });

            //                // Only return safe public fields (never password hash)
            //                return Results.Ok(new
            //                {
            //                    user.Id,
            //                    user.Email,
            //                    user.FullName,
            //                    user.CreatedAt
            //                });
            //            })
            //.RequireAuthorization();
            //        }

        }

    }
}