/****** Developed by: Fernando Calmet Ramirez <fercalmet@gmail.com> ******/
USE [cs-aspnet-mvc-crud]
-- Truncate Data
EXEC sp_MSForEachTable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'
EXEC sp_MSForEachTable 'DELETE FROM ?'
EXEC sp_MSForEachTable 'ALTER TABLE ? CHECK CONSTRAINT ALL'
DBCC CHECKIDENT ('module_category', RESEED, 0);
DBCC CHECKIDENT ('module', RESEED, 0);
DBCC CHECKIDENT ('user_action', RESEED, 0);
DBCC CHECKIDENT ('note', RESEED, 0);
DBCC CHECKIDENT ('task_note', RESEED, 0);
DBCC CHECKIDENT ('task', RESEED, 0);
DBCC CHECKIDENT ('user_position', RESEED, 0);
DBCC CHECKIDENT ('user_permission', RESEED, 0);
DBCC CHECKIDENT ('user', RESEED, 0);
GO

-- Insert data in table: Modules Categories
INSERT INTO [dbo].[module_category] (name, description) VALUES ('Security', 'Main module of the system that manages user information and the scope of their actions.');
INSERT INTO [dbo].[module_category] (name, description) VALUES ('General', 'Secondary module of the system that manages general information.');
INSERT INTO [dbo].[module_category] (name, description) VALUES ('Setting', 'This system module manages the application settings.');
INSERT INTO [dbo].[module_category] (name, description) VALUES ('Report', 'This system module shows the information reports.');

-- Insert data in table: Modules
---- Security Modules:
INSERT INTO [dbo].[module] (name, module_category_id) VALUES ('Modules Categories', 1);
INSERT INTO [dbo].[module] (name, module_category_id) VALUES ('Modules', 1);
INSERT INTO [dbo].[module] (name, module_category_id) VALUES ('Users Positions', 1);
INSERT INTO [dbo].[module] (name, module_category_id) VALUES ('Users Actions', 1);
INSERT INTO [dbo].[module] (name, module_category_id) VALUES ('Users Permissions', 1);
INSERT INTO [dbo].[module] (name, module_category_id) VALUES ('Users', 1);
---- General Modules:
INSERT INTO [dbo].[module] (name, module_category_id) VALUES ('Tasks', 2);
INSERT INTO [dbo].[module] (name, module_category_id) VALUES ('Notes', 2);
INSERT INTO [dbo].[module] (name, module_category_id) VALUES ('Task Notes', 2);

-- Insert data in table: Users Positions
INSERT INTO [dbo].[user_position] (name) VALUES ('Administrator');
INSERT INTO [dbo].[user_position] (name) VALUES ('Customer');

-- Insert data in table: Users Actions
---- Actions of Modules Categories
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View All Modules Categories', 1);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View Module Category', 1);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Create Module', 1);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Edit Module', 1);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Delete Module', 1);
---- Actions of Modules
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View All Modules', 2);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View Module', 2);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Create Module', 2);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Edit Module', 2);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Delete Module', 2);
---- Actions of Users Positions
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View All Users Positions', 3);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View User Position', 3);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Create User Position', 3);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Edit User Position', 3);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Delete User Position', 3);
---- Actions of Users Actions
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View All Users Actions', 4);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View User Action',4 );
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Create User Action', 4);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Edit User Action', 4);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Delete User Action', 4);
---- Actions of Users Permissions
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View All Users Permissions', 5);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View User Permission', 5);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Create User Permission', 5);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Edit User Permission', 5);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Delete User Permission', 5);
---- Actions of Users
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View All Users', 6);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View User', 6);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Create User', 6);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Edit User', 6);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Delete User', 6);
---- Actions of Tasks
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View All Tasks', 7);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View Task', 7);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Create Task', 7);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Edit Task', 7);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Delete Task', 7);
---- Actions of Notes
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View All Notes', 8);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View Note', 8);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Create Note', 8);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Edit Note', 8);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Delete Note', 8);
---- Actions of Tasks Notes
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View All Tasks Notes', 9);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('View Task Note', 9);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Create Task Note', 9);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Edit Task Note', 9);
INSERT INTO [dbo].[user_action] (name, module_id) VALUES ('Delete Task Note', 9);

-- Insert data in table: Users Permissions
---- Permissions for Administrators
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 1);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 2);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 3);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 4);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 5);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 6);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 7);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 8);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 9);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 10);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 11);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 12);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 13);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 14);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 15);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 16);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 17);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 18);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 19);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 20);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 21);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 22);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 23);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 24);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 25);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 26);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 27);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 28);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 29);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 30);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 31);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 32);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 33);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 34);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 35);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 36);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 37);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 38);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 39);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 40);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 41);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 42);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 43);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 44);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (1, 45);
---- Permissions for Customers
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 30);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 31);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 32);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 33);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 34);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 35);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 36);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 37);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 38);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 39);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 40);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 41);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 42);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 43);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 44);
INSERT INTO [dbo].[user_permission] (user_position_id, user_action_id) VALUES (2, 45);

-- Insert data in table: Users
INSERT INTO [dbo].[user] (
    username,
    email,
    email_confirmed,
    password_hash,
    security_stamp,
    two_factor_enabled,
    lockout_end_date_utc,
    lockout_enabled,
    access_failed_count,
    first_name,
    last_name,
    picture,
    birthdate,
    created_at,
    user_position_id
) VALUES (
    'fcalmetr',
    'fercalmet@gmail.com',
	1,
    '123456',
    'ABC123',
    0,
    null,
    0,
    0,
    'Fernando',
    'Calmet',
    null, 
    '01-01-1989', 
    getdate(), 
    1
);