CREATE TABLE
    default.user(
        id Int64,
        birth_date DATETIME,
        name String
    ) ENGINE = MergeTree()
ORDER BY (id) PRIMARY KEY(id);