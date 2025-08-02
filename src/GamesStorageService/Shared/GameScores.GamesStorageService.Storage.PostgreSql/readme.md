# Schema definition
```sql
CREATE TABLE IF NOT EXISTS games (
    id                  SERIAL          PRIMARY KEY,
    external_id         UUID            NOT NULL,
    sport_type          VARCHAR(25)     NOT NULL,
    competition_name    VARCHAR(255)    NOT NULL,
    event_date          TIMESTAMP       NOT NULL,
    teams               VARCHAR(50)[]   NOT NULL,
    created_at          TIMESTAMP       NOT NULL    DEFAULT (NOW() AT TIME ZONE 'UTC'),
    updated_at          TIMESTAMP
);

CREATE UNIQUE INDEX IF NOT EXISTS uq_games_external_id ON games (external_id);

CREATE INDEX IF NOT EXISTS idx_games_event_date ON games (event_date DESC);

```