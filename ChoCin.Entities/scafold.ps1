Remove-Item *.cs
dotnet ef dbcontext scaffold "server=localhost,3306;database=ChoCin;user=root;password=root" Pomelo.EntityFrameworkCore.MySql -d -f -c "ChocinDbContext" -v 
#dotnet ef dbcontext scaffold "Name=ConnectionStrings:ChocinDbContext" "MySql.EntityFrameworkCore" -d -f -c "ChocinDbContext" -v 
#dotnet ef dbcontext scaffold "server=localhost:3306;database=ChoCin;user=root;password=root" MySql.EntityFrameworkCore