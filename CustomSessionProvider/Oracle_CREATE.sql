CREATE TABLE "SESSIONS"
(
    "SESSIONID" NVARCHAR2(116) NOT NULL ENABLE,
    "APPLICATIONNAME" NVARCHAR2(255) NOT NULL ENABLE,
    "CREATED" timestamp DEFAULT SYS_EXTRACT_UTC(SYSTIMESTAMP) NOT NULL ENABLE,
    "EXPIRES" timestamp NOT NULL ENABLE,
    "LOCKDATE" timestamp NOT NULL ENABLE,
    "LOCKID"  NUMBER(*,0) NOT NULL ENABLE,
    "TIMEOUT" NUMBER(*,0) NOT NULL ENABLE,
    "LOCKED"  NUMBER(*,0) NOT NULL ENABLE,
    "SESSIONITEMS" long,
    "FLAGS" NUMBER(*,0) DEFAULT 0 NOT NULL ENABLE,
    PRIMARY KEY ("SESSIONID")
);