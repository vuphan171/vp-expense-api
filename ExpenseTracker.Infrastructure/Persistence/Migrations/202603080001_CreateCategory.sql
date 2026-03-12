DROP TABLE IF EXISTS public.category;

CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE TABLE public.category
(
    id          uuid PRIMARY KEY      DEFAULT gen_random_uuid(),

    customer_id uuid         NOT NULL
        REFERENCES public.customer (id)
            ON DELETE CASCADE,

    name        citext       NOT NULL CHECK (char_length(name) <= 50),
    description varchar(255) NOT NULL,

    is_active   boolean      NOT NULL DEFAULT true,

    created_at  timestamptz  NOT NULL DEFAULT now(),
    updated_at  timestamptz  NOT NULL DEFAULT now(),

    CONSTRAINT uq_category_customer_name
        UNIQUE (customer_id, name)
);


CREATE INDEX ix_category_customer_created_at
    ON public.category (customer_id, created_at DESC);