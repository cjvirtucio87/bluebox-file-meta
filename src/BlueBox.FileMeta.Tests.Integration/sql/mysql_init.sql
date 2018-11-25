DROP TABLE IF EXISTS counters;
DROP TABLE IF EXISTS part;
DROP TABLE IF EXISTS file;

CREATE TABLE counters (
    next_id INT NOT NULL,
    table_name VARCHAR(255) NOT NULL UNIQUE
);

CREATE TABLE file (
    Id INT PRIMARY KEY
);
CREATE TABLE part (
    Id INT PRIMARY KEY,
    FileId INT,
    FOREIGN KEY (FileId)
        REFERENCES file(Id)
        ON DELETE CASCADE,
    BlockId VARCHAR(255)
);

INSERT INTO counters(
  next_id,
  table_name
) VALUES (
  1,
  'file'
);

INSERT INTO counters(
  next_id,
  table_name
) VALUES (
  1,
  'part'
);