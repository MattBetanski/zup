CREATE OR REPLACE FUNCTION update_like_count() RETURNS TRIGGER AS $$
DECLARE
    true_votes int;
    false_votes int;
    total_rating int;
BEGIN
    select count(*) into true_votes from note_rating where rate = true and note_rating.note_id = NEW.note_id;
    select count(*) into false_votes from note_rating where rate = false and note_rating.note_id = NEW.note_id;
    total_rating := true_votes - false_votes;

    UPDATE note
    SET like_count = total_rating
    WHERE note_id = NEW.note_id;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_like_count_trigger
AFTER INSERT OR UPDATE OR DELETE ON note_rating
FOR EACH ROW EXECUTE PROCEDURE update_like_count();