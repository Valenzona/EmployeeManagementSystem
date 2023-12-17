CREATE TABLE users	
(
	id INT PRIMARY KEY IDENTITY(1,1),
	username VARCHAR(MAX) NULL,
	password VARCHAR(MAX) NULL,
	date_register DATE NULL

)

SELECT * FROM users

CREATE TABLE employees
(
	id INT PRIMARY KEY IDENTITY(1,1),
	employee_id VARCHAR(MAX) NULL,
	full_name VARCHAR(MAX) NULL,
	gender VARCHAR(MAX) NULL,
	contact_number VARCHAR(MAX) NULL,
	position VARCHAR(MAX) NULL,
	image VARCHAR(MAX) NULL,
	salary INT NULL,
	insert_date DATE NULL,
	update_date DATE NULL,
	delete_date DATE NULL,
)

SELECT * FROM employees

ALTER TABLE employees
ADD status VARCHAR(MAX) NULL

SELECT * FROM employees WHERE delete_date IS NOT NULL

DELETE FROM employees

INSERT INTO users (username, password, date_register, role)
VALUES ('admin1', 'adminpass', GETDATE(), 'Admin');

INSERT INTO users (username, password, date_register, role)
VALUES ('hr1', 'securepass', GETDATE(), 'HR');

INSERT INTO users (username, password, date_register, role)
VALUES ('accountant1', 'accountpass', GETDATE(), 'Accounting');

SELECT * FROM users

INSERT INTO employees (employee_id, full_name, gender, contact_number, position, image, salary, insert_date, update_date, delete_date, status)
VALUES ('EMP001', 'John Go', 'Male', '123-456-7890', 'Admin', 'image_path.jpg', 50000, GETDATE(), NULL, NULL, 'Active');

INSERT INTO employees (employee_id, full_name, gender, contact_number, position, image, salary, insert_date, update_date, delete_date, status)
VALUES ('EMP001', 'John Si', 'Male', '123-456-7890', 'Human Resource', 'image_path.jpg', 50000, GETDATE(), NULL, NULL, 'Active');

INSERT INTO employees (employee_id, full_name, gender, contact_number, position, image, salary, insert_date, update_date, delete_date, status)
VALUES ('EMP001', 'John Pablo', 'Male', '123-456-7890', 'Accountant', 'image_path.jpg', 50000, GETDATE(), NULL, NULL, 'Active');

SELECT * FROM employees