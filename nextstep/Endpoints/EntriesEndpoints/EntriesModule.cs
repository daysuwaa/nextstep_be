using System;
using Microsoft.EntityFrameworkCore;
using nextstep.application.DTOs.Responses;
using nextstep.infrastructure;
using nextstep.Models.Requests;
using nextstep.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Antiforgery;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Text;
using System.Security.Cryptography;

namespace nextstep.Endpoints.EntriesEndpoints
{
    public static class EntryModule
    {
        public static void AddEntriesEndpoints(this IEndpointRouteBuilder app)
        {
            // to get all entries for that particular user
            app.MapGet("api/v1/entries", async (AppDbContext db, HttpContext context) =>
            {
                var userId = UserHelper.GetUserIdFromContext(context);
                var entries = await db.Entries
                    .Where(e => e.UserId == userId)
                    .OrderByDescending(e => e.CreatedAt)
                    .ToListAsync();

                return Results.Ok(entries);
            }).RequireAuthorization();


            // to get one entry by id
            app.MapGet("api/v1/entries/{id}", async (int id, AppDbContext db) =>
            {
                var entry = await db.Entries.FindAsync(id);
                return entry is not null ? Results.Ok(entry) : Results.NotFound(new { message = "Entry not found" });
            });

            // GET the cloudinary signed signature
            app.MapGet("api/v1/cloudinary-signature", ([FromServices] IConfiguration config) =>
            {
                var cloudName = config["Cloudinary:CloudName"];
                var apiKey = config["Cloudinary:ApiKey"];
                var apiSecret = config["Cloudinary:ApiSecret"];

                // Generate a timestamp
                var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                // Cloudinary signed string format
                var toSign = $"timestamp={timestamp}{apiSecret}";

                using var sha1 = SHA1.Create();
                var hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(toSign));
                var signature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return Results.Ok(new
                {
                    cloudName,
                    apiKey,
                    timestamp,
                    signature
                });
            });

            // to add an entry for a user
            app.MapPost("api/v1/{userId}/entries", async ([FromBody] AddNewEntry entryRequest, AppDbContext db, HttpContext context) =>
            {
                if (string.IsNullOrWhiteSpace(entryRequest.Title))
                    return Results.BadRequest(new { message = "Title is required" });
                else if (string.IsNullOrWhiteSpace(entryRequest.ImageUrl))
                {
                    return Results.BadRequest(new { message = "Image is required" });
                }
                else if (string.IsNullOrWhiteSpace(entryRequest.Content))
                {
                    return Results.BadRequest(new { message = "Content is required" });
                }
                else if (string.IsNullOrWhiteSpace(entryRequest.Mood))
                {
                    return Results.BadRequest(new { message = "Mood is required" });
                }
                else if (string.IsNullOrWhiteSpace(entryRequest.Excerpt))
                {
                    return Results.BadRequest(new { message = "Excerpt is required" });
                }


                // Get the currently logged-in user’s ID from the HTTP request context.
                var userId = UserHelper.GetUserIdFromContext(context);

                var newEntry = new Entry
                {
                    Title = entryRequest.Title,
                    Mood = entryRequest.Mood,
                    Content = entryRequest.Content,
                    Excerpt = entryRequest.Excerpt,
                    ImageUrl = entryRequest.ImageUrl,
                    CreatedAt = DateTime.UtcNow,
                    UserId = userId,
                };

                await db.Entries.AddAsync(newEntry);
                await db.SaveChangesAsync();


                return Results.Created($"/api/v1/entries/{newEntry.Id}", newEntry);
            })
                .RequireAuthorization();
          



            // to delete a post
            app.MapDelete("api/v1/entries/{id}", async (int id, AppDbContext db) =>
            {
                // find entry based on id
                var entry = await db.Entries.FindAsync(id);
                // if entry is null, retun , entry not found
                if (entry == null) return Results.NotFound(new { message = "Entry not found" });

                // db removies entry
                db.Entries.Remove(entry);
                await db.SaveChangesAsync();
                // return a message entry deleted sucess fully
                return Results.Ok(new { message = "Entry deleted successfully" });
            });

            // to edit an entry
            app.MapPatch("api/v1/entries/{id}", async (int id,EditEntryReq updatedEntry, AppDbContext db) =>
            {
                var entry = await db.Entries.FindAsync(id);
                if (entry == null) return Results.NotFound(new { message = "Entry not found" });

                entry.Title = updatedEntry.Title;
                entry.Mood = updatedEntry.Mood;
                entry.Content = updatedEntry.Content;
                entry.Excerpt = updatedEntry.Excerpt;
                //entry.Image = updatedEntry.Image;
                //entry.CreatedAt = DateTime.Now;
                entry.UpdatedAt = DateTime.UtcNow;

                await db.SaveChangesAsync();
                return Results.Ok(entry);
            });

        }
    }
}