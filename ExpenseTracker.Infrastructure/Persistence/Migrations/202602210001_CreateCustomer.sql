DROP TABLE IF EXISTS public.customer;

CREATE EXTENSION IF NOT EXISTS pgcrypto;
CREATE EXTENSION IF NOT EXISTS citext;

CREATE TABLE public.customer
(
    id            uuid PRIMARY KEY      DEFAULT gen_random_uuid(),
    first_name    varchar(100) NOT NULL,
    last_name     varchar(100) NOT NULL,
    email         citext       NOT NULL,
    date_of_birth timestamp    NOT NULL,
    password_hash text         NOT NULL,
    is_active     boolean      NOT NULL DEFAULT true,
    created_at    timestamptz  NOT NULL DEFAULT now(),
    updated_at    timestamptz  NULL
);

ALTER TABLE public.customer
    ADD CONSTRAINT uq_customer_email UNIQUE (email);

CREATE INDEX ix_customer_created_at
    ON public.customer (created_at DESC);