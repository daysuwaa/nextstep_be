--
-- PostgreSQL database dump
--

\restrict IkxKdXM88RvY6udWvL0NR5lZCaeafWhZw7QF1kpFeE0rQNu0R7cl5ECNrQgHlIH

-- Dumped from database version 17.6 (Postgres.app)
-- Dumped by pg_dump version 18.0

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: entries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.entries (
    "Id" integer NOT NULL,
    "Title" text NOT NULL,
    "Mood" text,
    "Content" text,
    "Excerpt" text,
    "CreatedAt" timestamp with time zone DEFAULT now() NOT NULL,
    created_at timestamp without time zone DEFAULT now(),
    "UpdatedAt" timestamp with time zone,
    "ImageUrl" text,
    "UserId" integer
);


ALTER TABLE public.entries OWNER TO postgres;

--
-- Name: entries_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."entries_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."entries_Id_seq" OWNER TO postgres;

--
-- Name: entries_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."entries_Id_seq" OWNED BY public.entries."Id";


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    email character varying(255) NOT NULL,
    password character varying(255) NOT NULL,
    created_at timestamp without time zone DEFAULT now(),
    updated_at timestamp without time zone DEFAULT now()
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.users_id_seq OWNER TO postgres;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- Name: entries Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.entries ALTER COLUMN "Id" SET DEFAULT nextval('public."entries_Id_seq"'::regclass);


--
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- Data for Name: entries; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.entries ("Id", "Title", "Mood", "Content", "Excerpt", "CreatedAt", created_at, "UpdatedAt", "ImageUrl", "UserId") FROM stdin;
4	what a day we have	string	string	string	2025-10-10 08:51:38.769909+01	2025-10-10 08:51:38.771456	\N	\N	\N
20	string	string	stridddng	string	2025-10-12 18:18:15.649552+01	2025-10-12 18:18:15.819462	\N	\N	\N
38	rainy day	reflective	jlhhiijijojoko	heyyyy	2025-10-16 13:21:56.90196+01	2025-10-16 13:21:57.031677	\N	https://res.cloudinary.com/dnmbxwjth/image/upload/v1760617316/oabnr4ic2l6xl68bwkoh.jpg	2
21	jaden smith loves u 2	string	stridddng	string	2025-10-12 18:18:19.56821+01	2025-10-12 18:18:19.617335	2025-10-13 10:17:44.420693+01	\N	\N
24	crazy ass day man	string	string	string	2025-10-13 08:44:23.74152+01	2025-10-13 08:44:23.80416	2025-10-14 16:21:50.335596+01	\N	\N
26	what a great day to be alive	string	string	string	2025-10-14 16:30:51.140434+01	2025-10-14 16:30:51.566923	\N	\N	\N
27	what a great day to be alive	string	string	string	2025-10-14 16:45:22.373464+01	2025-10-14 16:45:22.437536	\N	https://res.cloudinary.com/demo/image/upload/sample.jpg	\N
28	ssss	sad	ssss	ssss	2025-10-14 20:07:39.606139+01	2025-10-14 20:07:39.716871	\N	https://res.cloudinary.com/dnmbxwjth/image/upload/v1760468859/y1hkwkybmphx5sqgpain.jpg	\N
29	what a great day to be alive yo	string	string	string	2025-10-14 20:46:40.567589+01	2025-10-14 20:46:40.86421	\N	https://res.cloudinary.com/demo/image/upload/sample.jpg	2
31	crazy ass day yoooo	string	string	string	2025-10-14 20:56:52.795761+01	2025-10-14 20:56:52.837995	2025-10-14 21:11:03.205755+01	https://res.cloudinary.com/dnmbxwjth/image/upload/v1760471812/fq3phbjewcq6tji7evk6.png	2
33	stolen phone today	sad	Guys, you wont believe what just happened, my phone was stolen im so sad. I was walking and then a guy walked up to me and stole my phone, i tried chasing him but that was futile. I am really sad and its the latest iphone, UGH	Omds , my phone got stolen today	2025-10-14 21:14:21.977656+01	2025-10-14 21:14:22.082385	\N	https://res.cloudinary.com/dnmbxwjth/image/upload/v1760472860/hazutsr95dqulsdsppxj.jpg	3
32	yoooom	sad	kmk,l'	okllm;kl	2025-10-14 20:56:56.381877+01	2025-10-14 20:56:56.392128	2025-10-15 08:50:39.033771+01	https://res.cloudinary.com/dnmbxwjth/image/upload/v1760471815/hyf1hyoqjfbffc6kan09.png	2
34	Im just trying sumn out	Happy	"use client";\nimport React, { useEffect, useState } from "react";\nimport JournalCardComponent from "./BlogCardComponent";\nimport EditModal from "../component/EditModal";\nimport DeleteModal from "../component/DeleteModal";\nimport ViewModal from "../component/ViewModal";\nimport Header from "../component/Header";\nimport toast from "react-hot-toast";\nimport axios from "axios";\n\ntype Entry = {\n  id: string;\n  title: string;\n  mood: string;\n\n   	Idk	2025-10-15 08:52:41.278658+01	2025-10-15 08:52:41.340588	2025-10-15 08:55:43.44833+01	https://res.cloudinary.com/dnmbxwjth/image/upload/v1760514761/s2d4e5gbn1hsv5wzhp2m.jpg	2
37	llll	calm	mmmmm	kkkk	2025-10-15 09:08:45.763837+01	2025-10-15 09:08:46.363049	2025-10-15 09:09:48.569067+01	https://res.cloudinary.com/dnmbxwjth/image/upload/v1760515725/hr0xsonfmfafwkyruyix.jpg	2
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (id, name, email, password, created_at, updated_at) FROM stdin;
2	adesuwa	adesuwa@email.com	$2a$11$Qtf6wKnM7hypbcuCOGKTAuI9qTGqY6a.KX.d7kkdGqzJQYx5.vGN6	2025-10-07 11:44:11.970406	-infinity
3	shikemi	shikemi@email.com	$2a$11$vS.85G2MNWrIt5/Pp.9qJOm3RHf6VKSm77CR8a3A.wWz1TXBUQoVm	2025-10-07 13:32:57.655426	-infinity
4	testuser	test@test.com	$2a$11$G5wyS.cvJZPNpCbsXzFiXOnBvMeohU7t9mObzx40w3n0KUddT9XTC	2025-10-07 14:48:42.602675	-infinity
5	johndoe	johndoe@example.com	$2a$11$FAaREf1RbdmTtnNiSAMmMe0nRdLQALthBerYCYl2pjVbP/x43wC.y	2025-10-07 15:43:31.939681	-infinity
\.


--
-- Name: entries_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."entries_Id_seq"', 39, true);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 5, true);


--
-- Name: entries entries_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.entries
    ADD CONSTRAINT entries_pkey PRIMARY KEY ("Id");


--
-- Name: users users_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- PostgreSQL database dump complete
--

\unrestrict IkxKdXM88RvY6udWvL0NR5lZCaeafWhZw7QF1kpFeE0rQNu0R7cl5ECNrQgHlIH

