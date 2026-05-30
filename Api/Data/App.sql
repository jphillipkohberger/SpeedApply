CREATE TABLE "Users" (
    "Id" integer NOT NULL,
    "UserName" character varying(50) NOT NULL,
    "Password" character varying(255) NOT NULL,
    "Email" character varying(255) NOT NULL,
    "CreatedAt" timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    "LastLogin" timestamp with time zone,
    "Address" text DEFAULT ''::text
);

CREATE TABLE "Queries" (
    "Id" SERIAL PRIMARY KEY,
    "Query" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UserId" INT,
    CONSTRAINT FkUser FOREIGN KEY ("UserId") REFERENCES "Users"("Id") ON DELETE CASCADE
);	

CREATE TABLE "RootUrls" (
    "Id" SERIAL PRIMARY KEY,
    "Domain" VARCHAR(100) UNIQUE NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

 CREATE TABLE "Files" (
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "CreatedAt" TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    "UserId" INT,
    CONSTRAINT FkUser FOREIGN KEY ("UserId") REFERENCES "Users"("Id") ON DELETE CASCADE
);

