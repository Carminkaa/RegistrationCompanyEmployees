# RegistrationCompanyEmployees
## Задача
 Разрабоать приложение для регистрации работников компаний. Для доступа к данным должен использоваться чистый ADO.NET
## SQL для создания таблиц
    CREATE TABLE companies
    (
    id INT PRIMARY KEY IDENTITY,
    title NVARCHAR(20) NOT NULL,
    OrganizationalForm INT
    );
    CREATE TABLE workers
    (
    Id INT PRIMARY KEY IDENTITY,
    last_name NVARCHAR(20),
    first_name NVARCHAR(20),
    patronymic NVARCHAR(20),
    date_receipt DATETIME,
    position INT,
    company_id INT REFERENCES companies (id),
);