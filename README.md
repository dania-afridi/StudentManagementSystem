# Student Management System (Console App)
Simple C# console application to manage students.

## Description
A C# console application to manage students 
using a menu-driven interface.
It supports full CRUD operations with input validation and 
persistent storage.


## Features
- Add student with validation
- List students sorted by name
- Filter students by minimum age
- Update student by ID
- Delete student by ID
- Persistent storage using JSON file
- Menu-based navigation

## Tech Stack
- C#
- .NET Console App
- LINQ
- JSON for data storage
- Git / GitHub

## Data Storage
- Week 1–2: JSON file storage using FileService
- SQL Server integration introduced but not yet wired to menu

## SQL Learning (Isolation)
- Parameterized INSERT, UPDATE, DELETE
- Unique constraint handling
- Tested in StudentDbService only
 
## SQL (Week 2 Day 5)
- Implemented parameterized INSERT and SELECT using ADO.NET
- Handled UNIQUE constraint violations safely
- Tested SQL logic in isolation without affecting menu flow

## SQL (Week 2 Day 6)
- Switched student listing to read from SQL Server
- Write operations still use existing storage

### Week 2 Day 7
- Migrated Add Student operation to SQL Server
- Removed JSON write for student creation
- SQL Server is now source of truth for ADD and READ

## How to Run
1. Clone the repository
2. Open solution in Visual Studio 2022
3. Run the console application


