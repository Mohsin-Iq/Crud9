Create procedure [dbo].[DeleteStudent]  
(  
   @StdId int  
)  
as   
begin  
   Delete from StudentReg where Id=@Id  
End


//Important ------------------------------------------------------------------------------------------------------------
USE [Student_Information]
GO
/****** Object:  StoredProcedure [dbo].[Login]    Script Date: 8/4/2023 12:16:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[Login]
(
@Email       NVARCHAR(100),
@PASSWORD    NVARCHAR(100)
)
AS 
BEGIN

INSERT INTO Student(Email,PASSWORD) VALUES (@Email,@PASSWORD)
END
//Important ------------------------------------------------------------------------------------------------------------
USE [Student_Information]
GO
/****** Object:  StoredProcedure [dbo].[GetAllBooks]    Script Date: 8/4/2023 12:16:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER procedure [dbo].[GetAllBooks]
as
begin 

Select BookName ,BookID  from Book
End 

//Important ------------------------------------------------------------------------------------------------------------
USE [Student_Information]
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudent]    Script Date: 8/4/2023 12:16:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[DeleteStudent]
(
@StudentID      int
)
AS 
BEGIN 

DELETE FROM StudentBook 
Where StudentID = @StudentID
DELETE FROM Student
Where StudentID = @StudentID

END

//Important ------------------------------------------------------------------------------------------------------------

USE [Student_Information]
GO
/****** Object:  StoredProcedure [dbo].[AddDetails]    Script Date: 8/4/2023 12:16:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddDetails] 
(
    @StudentID     INT=NULL,
    @StudentName   NVARCHAR(100),  
    @Password      NVARCHAR(100),  
    @Email         NVARCHAR(100),  
    @Phone         NVARCHAR(100),
	@bookIDs       NVARCHAR(100)
)
AS  
BEGIN

        if NOT exists(select 1 from Student where StudentID=@StudentID)
		begin
			INSERT INTO Student   (StudentName,Email,Phone,Password)
			VALUES (@StudentName, @Email, @Phone, @Password)
			
			Select @StudentID = SCOPE_IDENTITY()
		end
		else
		begin
			UPDATE S
			SET S.StudentName=@StudentName,
			S.Password=@Password,
			S.Email=@Email,
			S.Phone=@Phone
			FROM Student S
			where S.StudentID=@StudentID

			Delete From StudentBook Where StudentID = @StudentID

		end		
		DECLARE @BookIDTable TABLE (ID INT)

        INSERT INTO @BookIDTable (ID)
        SELECT CAST(Value AS INT)
        FROM STRING_SPLIT(@BookIDs, ',')

		INSERT INTO StudentBook (StudentID, BookID)
        SELECT @StudentID, ID
        FROM @BookIDTable
END    






