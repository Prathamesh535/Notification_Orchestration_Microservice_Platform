# Keka DB Migrator Instructions

We will use this project for writing the migration scripts for Keka DB. For this we will be using DbUp Nuget Package. DbUp will run all the scripts which are part of this project based on the file name. To ensure migration scripts are executed in the order, we need to maintain proper seed number. For consistency and to avoid conflicts, we are going to generate random seed number using this syntax in powershell or any other such similar tools. 

For generating SQL Migration file, please run this command from migrator directory. 

```powershell
.\AddMigration.ps1
```

## How to write migration scripts
1. Run the above command from migrator directory in powershell.			
1. This will prompt to "Enter Migration Name", please provide meaningful name for the migration script.
1. This will create a new migration script in the migrator project with the name provided in the previous step. File Prefix is automatically appended to ensure that the migration script is unique and executed in the chronological order
1. Please write the migration script in the file created in the previous step.
1. Please ensure that the migration script is idempotent.

## DBUp Helper Functions Documentation with Examples

DBUp is a tool for managing SQL migrations across different environments. To ensure robust and error-free database migrations, several helper functions have been implemented for checking the existence of SQL entities. These functions are idempotent, meaning they can be run multiple times without causing errors or unintended changes to the database schema. Below is the documentation for these helper functions, including example usages.

#### Function: `[dbo].[TypeExists]`
- **Parameters**: `@name SYSNAME` (Name of the type)
- **Description**: Checks if a specific SQL data type exists in the database.
- **Example Usage**: 
  ```sql
  IF [dbo].[TypeExists]('CustomDataType') = 0
  BEGIN
    -- Create or use CustomDataType
  END
  ```

#### Function: `[dbo].[TableExists]`
- **Parameters**: `@name SYSNAME` (Name of the table)
- **Description**: Determines if a table exists in the database.
- **Example Usage**: 
  ```sql
  IF [dbo].[TableExists]('Employee') = 0
  BEGIN
    -- Perform operations on the 'Employee' table
  END
  ```

#### Function: `[dbo].[ViewExists]`
- **Parameters**: `@name SYSNAME` (Name of the view)
- **Description**: Checks if a specific view exists in the database.
- **Example Usage**: 
  ```sql
  IF [dbo].[ViewExists]('EmployeeView') = 0
  BEGIN
    -- Create 'EmployeeView'
  END
  ```

#### Function: `[dbo].[StoredProcedureExists]`
- **Parameters**: `@name sysname` (Name of the stored procedure)
- **Description**: Verifies the existence of a stored procedure.
- **Example Usage**: 
  ```sql
  IF [dbo].[StoredProcedureExists]('UpdateEmployeeDetails') = 0
  BEGIN
    -- Create 'UpdateEmployeeDetails' Stored Procedure
  END
  ```

#### Function: `[dbo].[ColumnExists]`
- **Parameters**: 
  - `@table SYSNAME` (Name of the table)
  - `@name SYSNAME` (Name of the column)
- **Description**: Checks if a specific column exists in a given table.
- **Example Usage**: 
  ```sql
  IF [dbo].[ColumnExists]('Employee', 'EmailAddress') = 0
  BEGIN
    -- Add 'EmailAddress' column to 'Employee' table
  END
  ```

#### Function: `[dbo].[IndexExists]`
- **Parameters**: 
  - `@table SYSNAME` (Name of the table)
  - `@name SYSNAME` (Name of the index)
- **Description**: Determines if an index exists on a specified table.
- **Example Usage**: 
  ```sql
  IF [dbo].[IndexExists]('Employee', 'IX_Employee_Name') = 0
  BEGIN
    -- Create 'IX_Employee_Name' index on 'Employee' table
  END
  ```

#### Function: `[dbo].[FunctionExists]`
- **Parameters**: `@name SYSNAME` (Name of the function)
- **Description**: Checks for the existence of a specified function.
- **Example Usage**: 
  ```sql
  IF [dbo].[FunctionExists]('CalculateBonus') = 0
  BEGIN
    -- Define 'CalculateBonus' function
  END
  ```

#### Function: `[dbo].[ConstraintExists]`
- **Parameters**: 
  - `@table SYSNAME` (Name of the table)
  - `@name SYSNAME` (Name of the constraint)
- **Description**: Verifies if a constraint exists on a specified table.
- **Example Usage**: 
  ```sql
  IF [dbo].[ConstraintExists]('Employee', 'CK_Employee_Salary') = 0
  BEGIN
    -- Add 'CK_Employee_Salary' constraint to 'Employee' table
  END
  ```

---

#### Function: `[dbo].[PrimaryKeyExists]`
- **Parameters**: 
  - `@table sysname` (Name of the table)
  - `@name sysname` (Name of the primary key)
- **Description**: Determines if a primary key exists on a specified table.
- **Example Usage**: 
  ```sql
  IF [dbo].[PrimaryKeyExists]('Employee', 'PK_Employee') = 0
  BEGIN
    -- Set 'PK_Employee' as primary key for 'Employee' table
  END
  ```

---

#### Function: `[dbo].[SchemaExists]`

##### Description
This function checks whether a specified schema exists in the SQL Server database.

##### Parameters
- `@schemaName SYSNAME`: The name of the schema to check for existence.

##### Returns
- `1 (BIT)`: If the schema exists.
- `0 (BIT)`: If the schema does not exist.
- `NULL`: If the input parameter is `NULL`.

##### Example Usage
```sql
IF [dbo].[SchemaExists]('CustomSchema') = 0
BEGIN
    -- Create the schema if it doesn't exist
    EXEC('CREATE SCHEMA CustomSchema');
END
```

##### Remarks
Use this function to verify the existence of a schema before attempting to create it or perform operations that depend on its existence.

---

#### Function: `[dbo].[SecurityPolicyExists]`

##### Description
This function determines if a specified security policy exists in the SQL Server database.

##### Parameters
- `@policyName SYSNAME`: The name of the security policy to check for existence.

##### Returns
- `1 (BIT)`: If the security policy exists.
- `0 (BIT)`: If the security policy does not exist.
- `NULL`: If the input parameter is `NULL`.

##### Example Usage
```sql
IF [dbo].[SecurityPolicyExists]('DataAccessPolicy') = 0
BEGIN
    -- Create the security policy if it doesn't exist
    -- Example SQL code to create a security policy goes here
    PRINT 'Security policy created';
END
```

##### Remarks
This function is particularly useful in environments with complex security requirements. It can be used to ensure that security policies are not duplicated and are in place before applying security-based operations or modifications.

---