DROP TABLE IF EXISTS public.wallet;

CREATE TABLE public.wallet
(
    id          uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    customer_id uuid NOT NULL REFERENCES public.customer(id) ON DELETE CASCADE,
    wallet_name        varchar(100) NOT NULL,
    balance     numeric(18,2) NOT NULL DEFAULT 0,
    currency    varchar(10) NOT NULL DEFAULT 'USD',
    is_active   boolean NOT NULL DEFAULT true,
    created_at  timestamptz NOT NULL DEFAULT now(),
    updated_at  timestamptz NULL
);

CREATE INDEX ix_wallet_customer_id
    ON public.wallet (customer_id);

CREATE INDEX ix_wallet_created_at
    ON public.wallet (created_at DESC);

ALTER TABLE public.wallet
    ADD CONSTRAINT uq_wallet_customer_name UNIQUE (customer_id, wallet_name);