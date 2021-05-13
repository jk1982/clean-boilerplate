CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

create table if not exists users (
    id serial primary key,
    email varchar(100) not null,
    password_hash varchar(100) not null,
    blocked bool not null default true,
    created_at timestamp not null default current_timestamp,
	updated_at timestamp,
    last_access timestamp
);

