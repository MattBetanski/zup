CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE department (
    department_id serial NOT NULL,
    name text NOT NULL,
    CONSTRAINT "PK_department" PRIMARY KEY (department_id)
);

CREATE TABLE project (
    project_id serial NOT NULL,
    name text NOT NULL,
    fk_department_id integer NOT NULL,
    department_id integer,
    CONSTRAINT "PK_project" PRIMARY KEY (project_id),
    CONSTRAINT "FK_project_department_department_id" FOREIGN KEY (department_id) REFERENCES department (department_id)
);

CREATE INDEX "IX_project_department_id" ON project (department_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240423224205_initial_db', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE project DROP COLUMN fk_department_id;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240423225346_db-v0.1', '8.0.4');

COMMIT;

START TRANSACTION;

CREATE TABLE "user" (
    user_id serial NOT NULL,
    username text NOT NULL,
    hashed_password text NOT NULL,
    email text NOT NULL,
    first_name text,
    CONSTRAINT "PK_user" PRIMARY KEY (user_id)
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240429194759_usersmodel', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE project DROP CONSTRAINT "FK_project_department_department_id";

UPDATE "user" SET first_name = '' WHERE first_name IS NULL;
ALTER TABLE "user" ALTER COLUMN first_name SET NOT NULL;
ALTER TABLE "user" ALTER COLUMN first_name SET DEFAULT '';

ALTER TABLE "user" ALTER COLUMN user_id TYPE bigint;

ALTER TABLE "user" ADD account_activated boolean NOT NULL DEFAULT FALSE;

ALTER TABLE "user" ADD join_date timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

ALTER TABLE "user" ADD last_name text NOT NULL DEFAULT '';

ALTER TABLE "user" ADD salt text NOT NULL DEFAULT '';

ALTER TABLE project ALTER COLUMN department_id TYPE bigint;
UPDATE project SET department_id = 0 WHERE department_id IS NULL;
ALTER TABLE project ALTER COLUMN department_id SET NOT NULL;
ALTER TABLE project ALTER COLUMN department_id SET DEFAULT 0;

ALTER TABLE project ALTER COLUMN project_id TYPE bigint;

ALTER TABLE project ADD created_date timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

ALTER TABLE project ADD description text;

ALTER TABLE department ALTER COLUMN department_id TYPE bigint;

ALTER TABLE department ADD creation_date timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

ALTER TABLE department ADD description text;

ALTER TABLE department ADD visibility boolean NOT NULL DEFAULT FALSE;

CREATE TABLE item_history (
    history_id bigserial NOT NULL,
    item_id bigint NOT NULL,
    project_id bigint NOT NULL,
    owner_id bigint NOT NULL,
    parent_id bigint NOT NULL,
    name text NOT NULL,
    description text,
    type integer NOT NULL,
    confidentiality boolean NOT NULL,
    created_date timestamp with time zone NOT NULL,
    deadline timestamp with time zone,
    state integer NOT NULL,
    change text NOT NULL,
    change_user_id bigint NOT NULL,
    CONSTRAINT "PK_item_history" PRIMARY KEY (history_id),
    CONSTRAINT "FK_item_history_user_change_user_id" FOREIGN KEY (change_user_id) REFERENCES "user" (user_id) ON DELETE CASCADE,
    CONSTRAINT "FK_item_history_user_owner_id" FOREIGN KEY (owner_id) REFERENCES "user" (user_id) ON DELETE CASCADE
);

CREATE INDEX "IX_item_history_change_user_id" ON item_history (change_user_id);

CREATE INDEX "IX_item_history_owner_id" ON item_history (owner_id);

ALTER TABLE project ADD CONSTRAINT "FK_project_department_department_id" FOREIGN KEY (department_id) REFERENCES department (department_id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240506131953_Ddv2', '8.0.4');

COMMIT;

START TRANSACTION;

CREATE TABLE item (
    item_id bigserial NOT NULL,
    project_id bigint NOT NULL,
    owner_id bigint NOT NULL,
    parent_id bigint,
    "Name" text NOT NULL,
    description text,
    type integer NOT NULL,
    confidentiality boolean NOT NULL,
    created_date timestamp with time zone NOT NULL,
    deadline timestamp with time zone,
    state integer NOT NULL,
    CONSTRAINT "PK_item" PRIMARY KEY (item_id),
    CONSTRAINT "FK_item_item_parent_id" FOREIGN KEY (parent_id) REFERENCES item (item_id),
    CONSTRAINT "FK_item_project_project_id" FOREIGN KEY (project_id) REFERENCES project (project_id) ON DELETE CASCADE,
    CONSTRAINT "FK_item_user_owner_id" FOREIGN KEY (owner_id) REFERENCES "user" (user_id) ON DELETE CASCADE
);

CREATE TABLE item_comment (
    item_comment_id bigserial NOT NULL,
    user_id bigint NOT NULL,
    item_id bigint NOT NULL,
    comment text NOT NULL,
    comment_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_item_comment" PRIMARY KEY (item_comment_id)
);

CREATE TABLE note (
    note_id bigserial NOT NULL,
    owner_id bigint NOT NULL,
    title text NOT NULL,
    content text NOT NULL,
    created_date timestamp with time zone NOT NULL,
    like_count integer NOT NULL,
    department_id bigint NOT NULL,
    project_id bigint NOT NULL,
    CONSTRAINT "PK_note" PRIMARY KEY (note_id),
    CONSTRAINT "FK_note_department_department_id" FOREIGN KEY (department_id) REFERENCES department (department_id) ON DELETE CASCADE,
    CONSTRAINT "FK_note_project_project_id" FOREIGN KEY (project_id) REFERENCES project (project_id) ON DELETE CASCADE,
    CONSTRAINT "FK_note_user_owner_id" FOREIGN KEY (owner_id) REFERENCES "user" (user_id) ON DELETE CASCADE
);

CREATE TABLE role (
    role_id serial NOT NULL,
    department_id bigint NOT NULL,
    name text NOT NULL,
    description text,
    read_level integer NOT NULL,
    write_level integer NOT NULL,
    create_level integer NOT NULL,
    delete_level integer NOT NULL,
    wiki_level integer NOT NULL,
    wiki_delete boolean NOT NULL,
    CONSTRAINT "PK_role" PRIMARY KEY (role_id),
    CONSTRAINT "FK_role_department_department_id" FOREIGN KEY (department_id) REFERENCES department (department_id) ON DELETE CASCADE
);

CREATE TABLE wiki_page (
    wiki_page_id bigserial NOT NULL,
    project_id bigint NOT NULL,
    title text NOT NULL,
    file_path text NOT NULL,
    created_date timestamp with time zone NOT NULL,
    CONSTRAINT "PK_wiki_page" PRIMARY KEY (wiki_page_id),
    CONSTRAINT "FK_wiki_page_project_project_id" FOREIGN KEY (project_id) REFERENCES project (project_id) ON DELETE CASCADE
);

CREATE TABLE note_rating (
    note_rating_id bigserial NOT NULL,
    user_id bigint NOT NULL,
    note_id bigint NOT NULL,
    rate boolean NOT NULL,
    CONSTRAINT "PK_note_rating" PRIMARY KEY (note_rating_id),
    CONSTRAINT "FK_note_rating_note_note_id" FOREIGN KEY (note_id) REFERENCES note (note_id) ON DELETE CASCADE,
    CONSTRAINT "FK_note_rating_user_user_id" FOREIGN KEY (user_id) REFERENCES "user" (user_id) ON DELETE CASCADE
);

CREATE INDEX "IX_item_owner_id" ON item (owner_id);

CREATE INDEX "IX_item_parent_id" ON item (parent_id);

CREATE INDEX "IX_item_project_id" ON item (project_id);

CREATE INDEX "IX_note_department_id" ON note (department_id);

CREATE INDEX "IX_note_owner_id" ON note (owner_id);

CREATE INDEX "IX_note_project_id" ON note (project_id);

CREATE INDEX "IX_note_rating_note_id" ON note_rating (note_id);

CREATE INDEX "IX_note_rating_user_id" ON note_rating (user_id);

CREATE INDEX "IX_role_department_id" ON role (department_id);

CREATE INDEX "IX_wiki_page_project_id" ON wiki_page (project_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240506133530_Dbv2-1', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240506134053_Dbv2-2', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE role ALTER COLUMN role_id TYPE bigint;

CREATE TABLE department_member (
    member_id bigint NOT NULL,
    department_id bigint NOT NULL,
    CONSTRAINT "PK_department_member" PRIMARY KEY (member_id, department_id),
    CONSTRAINT "FK_department_member_department_department_id" FOREIGN KEY (department_id) REFERENCES department (department_id) ON DELETE CASCADE,
    CONSTRAINT "FK_department_member_user_member_id" FOREIGN KEY (member_id) REFERENCES "user" (user_id) ON DELETE CASCADE
);

CREATE TABLE item_assignee (
    item_id bigint NOT NULL,
    assingee_id bigint NOT NULL,
    CONSTRAINT "PK_item_assignee" PRIMARY KEY (item_id, assingee_id),
    CONSTRAINT "FK_item_assignee_item_item_id" FOREIGN KEY (item_id) REFERENCES item (item_id) ON DELETE CASCADE,
    CONSTRAINT "FK_item_assignee_user_assingee_id" FOREIGN KEY (assingee_id) REFERENCES "user" (user_id) ON DELETE CASCADE
);

CREATE TABLE "ProjectUserRole" (
    project_id bigint NOT NULL,
    user_id bigint NOT NULL,
    role_id bigint NOT NULL,
    CONSTRAINT "PK_ProjectUserRole" PRIMARY KEY (project_id, user_id, role_id),
    CONSTRAINT "FK_ProjectUserRole_project_project_id" FOREIGN KEY (project_id) REFERENCES project (project_id) ON DELETE CASCADE,
    CONSTRAINT "FK_ProjectUserRole_role_role_id" FOREIGN KEY (role_id) REFERENCES role (role_id) ON DELETE CASCADE,
    CONSTRAINT "FK_ProjectUserRole_user_user_id" FOREIGN KEY (user_id) REFERENCES "user" (user_id) ON DELETE CASCADE
);

CREATE INDEX "IX_department_member_department_id" ON department_member (department_id);

CREATE INDEX "IX_item_assignee_assingee_id" ON item_assignee (assingee_id);

CREATE INDEX "IX_ProjectUserRole_role_id" ON "ProjectUserRole" (role_id);

CREATE INDEX "IX_ProjectUserRole_user_id" ON "ProjectUserRole" (user_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240506134810_Dbv2-3', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240506134841_Dbv2-3-1', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240506190244_Dbv2-4', '8.0.4');

COMMIT;

START TRANSACTION;

CREATE TABLE department_invite (
    department_id bigint NOT NULL,
    invitee_id bigint NOT NULL,
    "Response" integer NOT NULL,
    CONSTRAINT "PK_department_invite" PRIMARY KEY (department_id, invitee_id),
    CONSTRAINT "FK_department_invite_department_department_id" FOREIGN KEY (department_id) REFERENCES department (department_id) ON DELETE CASCADE,
    CONSTRAINT "FK_department_invite_user_invitee_id" FOREIGN KEY (invitee_id) REFERENCES "user" (user_id) ON DELETE CASCADE
);

CREATE INDEX "IX_department_invite_invitee_id" ON department_invite (invitee_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240506190645_Dbv2-4-1', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE department_invite RENAME COLUMN "Response" TO response;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240506190739_Dbv2-4-2', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE wiki_page DROP CONSTRAINT "FK_wiki_page_project_project_id";

ALTER TABLE wiki_page DROP COLUMN file_path;

ALTER TABLE "user" DROP COLUMN salt;

ALTER TABLE wiki_page RENAME COLUMN project_id TO department_id;

ALTER INDEX "IX_wiki_page_project_id" RENAME TO "IX_wiki_page_department_id";

ALTER TABLE wiki_page ADD content text;

ALTER TABLE department ADD owner_id bigint NOT NULL DEFAULT 0;

CREATE INDEX "IX_department_owner_id" ON department (owner_id);

ALTER TABLE department ADD CONSTRAINT "FK_department_user_owner_id" FOREIGN KEY (owner_id) REFERENCES "user" (user_id) ON DELETE CASCADE;

ALTER TABLE wiki_page ADD CONSTRAINT "FK_wiki_page_department_department_id" FOREIGN KEY (department_id) REFERENCES department (department_id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240510174228_Db-v2-4-3', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240510174417_Db-v2-4-3-1', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240510174912_Db-v2-5', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240510175331_Db-v2-5-1', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240510175856_Db-v2-5-2', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240510180220_Db-v2-5-3', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE role DROP COLUMN create_level;

ALTER TABLE role DROP COLUMN delete_level;

ALTER TABLE role DROP COLUMN read_level;

ALTER TABLE role RENAME COLUMN write_level TO item_level;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240510195502_Dbv2-5-4', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240510204820_Dbv2-5-5', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE item_history DROP COLUMN confidentiality;

ALTER TABLE item_history DROP COLUMN deadline;

ALTER TABLE item DROP COLUMN confidentiality;

ALTER TABLE item DROP COLUMN deadline;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240511170252_Dbv2-5-6', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "ProjectUserRole" DROP CONSTRAINT "FK_ProjectUserRole_project_project_id";

ALTER TABLE "ProjectUserRole" DROP CONSTRAINT "FK_ProjectUserRole_role_role_id";

ALTER TABLE "ProjectUserRole" DROP CONSTRAINT "FK_ProjectUserRole_user_user_id";

ALTER TABLE "ProjectUserRole" DROP CONSTRAINT "PK_ProjectUserRole";

ALTER TABLE "ProjectUserRole" RENAME TO project_user_role;

ALTER INDEX "IX_ProjectUserRole_user_id" RENAME TO "IX_project_user_role_user_id";

ALTER INDEX "IX_ProjectUserRole_role_id" RENAME TO "IX_project_user_role_role_id";

ALTER TABLE project_user_role ADD CONSTRAINT "PK_project_user_role" PRIMARY KEY (project_id, user_id, role_id);

ALTER TABLE project_user_role ADD CONSTRAINT "FK_project_user_role_project_project_id" FOREIGN KEY (project_id) REFERENCES project (project_id) ON DELETE CASCADE;

ALTER TABLE project_user_role ADD CONSTRAINT "FK_project_user_role_role_role_id" FOREIGN KEY (role_id) REFERENCES role (role_id) ON DELETE CASCADE;

ALTER TABLE project_user_role ADD CONSTRAINT "FK_project_user_role_user_user_id" FOREIGN KEY (user_id) REFERENCES "user" (user_id) ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240511190623_Dbv2-5-7', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE note DROP CONSTRAINT "FK_note_project_project_id";

ALTER TABLE note RENAME COLUMN project_id TO "ProjectId";

ALTER INDEX "IX_note_project_id" RENAME TO "IX_note_ProjectId";

ALTER TABLE note ALTER COLUMN "ProjectId" DROP NOT NULL;

ALTER TABLE note ADD CONSTRAINT "FK_note_project_ProjectId" FOREIGN KEY ("ProjectId") REFERENCES project (project_id);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240515013034_Dbv2-5-8', '8.0.4');

COMMIT;

START TRANSACTION;


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
            

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240515014137_note_trigger', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240515025000_Dbv3-0-0', '8.0.4');

COMMIT;

