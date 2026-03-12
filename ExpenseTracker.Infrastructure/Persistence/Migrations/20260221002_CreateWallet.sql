CREATE EXTENSION IF NOT EXISTS citext;

DROP TABLE IF EXISTS public.wallet;

CREATE TABLE public.wallet
(
    id          uuid PRIMARY KEY        DEFAULT gen_random_uuid(),

    customer_id uuid           NOT NULL
        REFERENCES public.customer (id)
            ON DELETE CASCADE,

    name        citext         NOT NULL CHECK (char_length(name) <= 50),

    balance     numeric(18, 2) NOT NULL DEFAULT 0,

    currency    varchar(10)    NOT NULL DEFAULT 'USD',

    is_active   boolean        NOT NULL DEFAULT true,

    created_at  timestamptz    NOT NULL DEFAULT now(),

    updated_at  timestamptz    NOT NULL DEFAULT now(),

    CONSTRAINT uq_wallet_customer_name
        UNIQUE (customer_id, name)
);

CREATE INDEX ix_wallet_customer_created_at
    ON public.wallet (customer_id, created_at DESC);