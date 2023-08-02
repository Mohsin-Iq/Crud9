Create procedure [dbo].[DeleteStudent]  
(  
   @StdId int  
)  
as   
begin  
   Delete from StudentReg where Id=@Id  
End
