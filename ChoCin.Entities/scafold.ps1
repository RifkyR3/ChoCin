Remove-Item *.cs
dotnet ef dbcontext scaffold "server=localhost,3306;database=chocin;user=root;password=root" Pomelo.EntityFrameworkCore.MySql -d -f -c "ChocinDbContext" -v --no-onconfiguring