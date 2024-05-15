CREATE OR REPLACE FUNCTION update_like_count() RETURNS TRIGGER AS $$
BEGIN
    UPDATE note
    SET like_count = (
        SELECT COUNT(*)
        FROM note_rating
        WHERE note.note_id = note_rating.note_id AND rate = TRUE
    )
    WHERE note_id = NEW.note_id OR note_id = OLD.note_id;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_like_count_trigger
AFTER INSERT OR UPDATE OR DELETE ON note_rating
FOR EACH ROW EXECUTE PROCEDURE update_like_count();


CREAT OR REPLACE FUNCTION update_like_count() RETURNS TRIGGER AS $$
BEGIN
    --get sum of ratings where true rate is +1 and false rate is -1
    UPDATE note
    SET like_count = (
        SELECT COUNT(*)
        FROM note_rating
        WHERE note.note_id = note_rating.note_id AND rate = TRUE
    )
    WHERE note_id = NEW.note_id OR note_id = OLD.note_id;

    IF (TG_OP = 'INSERT' OR TG_OP = 'UPDATE') THEN
        --if rating is true, increment like_count
        
    ELSE
        UPDATE note
        SET like_count = (
            SELECT COUNT(*)
            FROM note_rating
            WHERE note.note_id = note_rating.note_id AND rate = TRUE
        )
        WHERE note_id = OLD.note_id;
    END IF;