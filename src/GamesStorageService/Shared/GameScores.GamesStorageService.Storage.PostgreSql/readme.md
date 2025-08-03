# Schema definition
```sql
CREATE TABLE IF NOT EXISTS sport_types (
    id          SMALLSERIAL PRIMARY KEY,
    name        VARCHAR(25) NOT NULL,
    created_at  TIMESTAMP   NOT NULL    DEFAULT (NOW() AT TIME ZONE 'UTC'),
    CONSTRAINT uq_sport_types_name
        UNIQUE (name)
);

CREATE TABLE IF NOT EXISTS competitions (
    id          SMALLSERIAL     PRIMARY KEY,
    name        VARCHAR(255)    NOT NULL,
    created_at  TIMESTAMP       NOT NULL    DEFAULT (NOW() AT TIME ZONE 'UTC'),
    CONSTRAINT uq_competitions_name
        UNIQUE (name)
);

CREATE TABLE IF NOT EXISTS games (
    id                  SERIAL          PRIMARY KEY,
    external_id         UUID            NOT NULL,
    sport_type_id       SMALLINT        NOT NULL,
    competition_id      SMALLINT        NOT NULL,
    event_date          TIMESTAMP       NOT NULL,
    teams               VARCHAR(50)[]   NOT NULL,
    created_at          TIMESTAMP       NOT NULL    DEFAULT (NOW() AT TIME ZONE 'UTC'),
    updated_at          TIMESTAMP,
    CONSTRAINT fk_games_sport_type
        FOREIGN KEY (sport_type_id)
        REFERENCES sport_types (id),
    CONSTRAINT fk_games_competition
        FOREIGN KEY (competition_id)
        REFERENCES competitions (id),
    CONSTRAINT uq_games_external_id
        UNIQUE (external_id)
    );
CREATE INDEX IF NOT EXISTS idx_games_event_date ON games (event_date DESC);

```