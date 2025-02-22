CREATE TABLE SchoolClass (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL
);
GO
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(255) NOT NULL,
    LastName NVARCHAR(255) NOT NULL,
    UserType INT NOT NULL, 
    Email NVARCHAR(255) UNIQUE NOT NULL,
    Phone NVARCHAR(20) NOT NULL,
    Active BIT NOT NULL DEFAULT 1
);
GO
CREATE TABLE ParentStudent (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ParentId INT NOT NULL,
    StudentId INT NOT NULL,
    CONSTRAINT FK_ParentStudent_Parent FOREIGN KEY (ParentId) REFERENCES [Users](Id),
    CONSTRAINT FK_ParentStudent_Student FOREIGN KEY (StudentId) REFERENCES [Users](Id)
);
GO
CREATE TABLE StudentClass (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    ClassId INT NOT NULL,
    CONSTRAINT FK_StudentClass_Student FOREIGN KEY (StudentId) REFERENCES [Users](Id),
    CONSTRAINT FK_StudentClass_Class FOREIGN KEY (ClassId) REFERENCES SchoolClass(Id)
);
GO

INSERT INTO SchoolClass (Name) VALUES 
('Class 1'), 
('Class 2'), 
('Class 3'), 
('Class 4'), 
('Class 5');
GO


INSERT INTO Users (FirstName, LastName, UserType, Email, Phone, Active) VALUES
('rahul', 'kharapkar', 2, 'rahul.kharapkar@yopmail.com', '9876543214', 1),
('Aarav', 'Sharma', 1, 'aarav.sharma@yopmail.com', '9876543210', 1),
('Ishita', 'Verma', 1, 'ishita.verma@yopmail.com', '9876543211', 1), 
('Rajesh', 'Patel', 2, 'rajesh.patel@yopmail.com', '9876543212', 1), 
('Sunita', 'Gupta', 2, 'sunita.gupta@yopmail.com', '9876543213', 1), 
('Kabir', 'Reddy', 1, 'kabir.reddy@yopmail.com', '9876543214', 1), 
('Neha', 'Singh', 1, 'neha.singh@yopmail.com', '9876543215', 1), 
('Amit', 'Mishra', 2, 'amit.mishra@yopmail.com', '9876543216', 1),
('Pooja', 'Chopra', 2, 'pooja.chopra@yopmail.com', '9876543217', 1); 
GO
select * from Users

INSERT INTO ParentStudent (ParentId, StudentId) VALUES 
(9, 6),
(3, 1), 
(3, 2), 
(4, 5); 
GO
INSERT INTO StudentClass (StudentId, ClassId) VALUES 
(6, 2),
(1, 1),  
(2, 2),  
(5, 3);  
GO
select * from SchoolClass
select * from Users
select * from ParentStudent
select * from StudentClass

SELECT 
    student.Id AS StudentId,
    CONCAT(student.FirstName, ' ', student.LastName) AS StudentName,
    CONCAT(parent.FirstName, ' ', parent.LastName) AS ParentName,
    schoolClass.Name AS StudentClass,
    parent.Email AS ParentEmail,
    parent.Phone AS ParentMobile,
    student.Active AS IsActive
FROM Users AS student
JOIN ParentStudent AS parentStudent ON student.Id = parentStudent.StudentId
JOIN Users AS parent ON parentStudent.ParentId = parent.Id
JOIN StudentClass AS studentClass ON student.Id = studentClass.StudentId
JOIN SchoolClass AS schoolClass ON studentClass.ClassId = schoolClass.Id
WHERE student.UserType = 1;