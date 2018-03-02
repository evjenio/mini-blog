-- User: "blogUser"
-- DROP USER "blogUser";

CREATE USER "blogUser" WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  NOCREATEDB
  NOCREATEROLE
  NOREPLICATION;
  
  -- Database: blog

-- DROP DATABASE blog;

CREATE DATABASE blog
    WITH 
    OWNER = "blogUser"
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Russia.1251'
    LC_CTYPE = 'Russian_Russia.1251'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1;

--
-- PostgreSQL database dump
--

-- Dumped from database version 10.3
-- Dumped by pg_dump version 10.3

-- Started on 2018-03-02 17:20:06

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 1 (class 3079 OID 12924)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2826 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 197 (class 1259 OID 16397)
-- Name: articles; Type: TABLE; Schema: public; Owner: blogUser
--

CREATE TABLE public.articles (
    id integer NOT NULL,
    header text,
    content text,
    imageid integer
);


ALTER TABLE public.articles OWNER TO "blogUser";

--
-- TOC entry 196 (class 1259 OID 16395)
-- Name: articles_id_seq; Type: SEQUENCE; Schema: public; Owner: blogUser
--

CREATE SEQUENCE public.articles_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.articles_id_seq OWNER TO "blogUser";

--
-- TOC entry 2827 (class 0 OID 0)
-- Dependencies: 196
-- Name: articles_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: blogUser
--

ALTER SEQUENCE public.articles_id_seq OWNED BY public.articles.id;


--
-- TOC entry 199 (class 1259 OID 16408)
-- Name: comments; Type: TABLE; Schema: public; Owner: blogUser
--

CREATE TABLE public.comments (
    id integer NOT NULL,
    text text,
    "user" text,
    articleid integer NOT NULL
);


ALTER TABLE public.comments OWNER TO "blogUser";

--
-- TOC entry 198 (class 1259 OID 16406)
-- Name: comments_id_seq; Type: SEQUENCE; Schema: public; Owner: blogUser
--

CREATE SEQUENCE public.comments_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.comments_id_seq OWNER TO "blogUser";

--
-- TOC entry 2828 (class 0 OID 0)
-- Dependencies: 198
-- Name: comments_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: blogUser
--

ALTER SEQUENCE public.comments_id_seq OWNED BY public.comments.id;


--
-- TOC entry 201 (class 1259 OID 16425)
-- Name: images; Type: TABLE; Schema: public; Owner: blogUser
--

CREATE TABLE public.images (
    id integer NOT NULL,
    image bytea
);


ALTER TABLE public.images OWNER TO "blogUser";

--
-- TOC entry 200 (class 1259 OID 16423)
-- Name: images_id_seq; Type: SEQUENCE; Schema: public; Owner: blogUser
--

CREATE SEQUENCE public.images_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.images_id_seq OWNER TO "blogUser";

--
-- TOC entry 2829 (class 0 OID 0)
-- Dependencies: 200
-- Name: images_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: blogUser
--

ALTER SEQUENCE public.images_id_seq OWNED BY public.images.id;


--
-- TOC entry 2685 (class 2604 OID 16400)
-- Name: articles id; Type: DEFAULT; Schema: public; Owner: blogUser
--

ALTER TABLE ONLY public.articles ALTER COLUMN id SET DEFAULT nextval('public.articles_id_seq'::regclass);


--
-- TOC entry 2686 (class 2604 OID 16411)
-- Name: comments id; Type: DEFAULT; Schema: public; Owner: blogUser
--

ALTER TABLE ONLY public.comments ALTER COLUMN id SET DEFAULT nextval('public.comments_id_seq'::regclass);


--
-- TOC entry 2687 (class 2604 OID 16428)
-- Name: images id; Type: DEFAULT; Schema: public; Owner: blogUser
--

ALTER TABLE ONLY public.images ALTER COLUMN id SET DEFAULT nextval('public.images_id_seq'::regclass);


--
-- TOC entry 2689 (class 2606 OID 16405)
-- Name: articles articles_pkey; Type: CONSTRAINT; Schema: public; Owner: blogUser
--

ALTER TABLE ONLY public.articles
    ADD CONSTRAINT articles_pkey PRIMARY KEY (id);


--
-- TOC entry 2692 (class 2606 OID 16416)
-- Name: comments comments_pkey; Type: CONSTRAINT; Schema: public; Owner: blogUser
--

ALTER TABLE ONLY public.comments
    ADD CONSTRAINT comments_pkey PRIMARY KEY (id);


--
-- TOC entry 2695 (class 2606 OID 16433)
-- Name: images images_pkey; Type: CONSTRAINT; Schema: public; Owner: blogUser
--

ALTER TABLE ONLY public.images
    ADD CONSTRAINT images_pkey PRIMARY KEY (id);


--
-- TOC entry 2693 (class 1259 OID 16422)
-- Name: fki_articleId_fk; Type: INDEX; Schema: public; Owner: blogUser
--

CREATE INDEX "fki_articleId_fk" ON public.comments USING btree (articleid);


--
-- TOC entry 2690 (class 1259 OID 16439)
-- Name: fki_imageId_fk; Type: INDEX; Schema: public; Owner: blogUser
--

CREATE INDEX "fki_imageId_fk" ON public.articles USING btree (imageid);


--
-- TOC entry 2697 (class 2606 OID 16417)
-- Name: comments articleId_fk; Type: FK CONSTRAINT; Schema: public; Owner: blogUser
--

ALTER TABLE ONLY public.comments
    ADD CONSTRAINT "articleId_fk" FOREIGN KEY (articleid) REFERENCES public.articles(id);


--
-- TOC entry 2696 (class 2606 OID 16434)
-- Name: articles imageId_fk; Type: FK CONSTRAINT; Schema: public; Owner: blogUser
--

ALTER TABLE ONLY public.articles
    ADD CONSTRAINT "imageId_fk" FOREIGN KEY (imageid) REFERENCES public.images(id);


-- Completed on 2018-03-02 17:20:06

--
-- PostgreSQL database dump complete
--

