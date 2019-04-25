using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DGStore.Data.Migrations
{
    public partial class addFirstDataInProductTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 520, N'توضیحات محصول نمونه 1', N'محصول نمونه 1', 250000)
INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 480, N'توضیحات محصول نمونه 2', N'محصول نمونه 2', 180000)
INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 300, N'توضیحات محصول نمونه 3', N'محصول نمونه 3', 150000)
INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 580, N'توضیحات محصول نمونه 4', N'محصول نمونه 4', 320000)
INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 170, N'توضیحات محصول نمونه 5', N'محصول نمونه 5', 200000)
INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 650, N'توضیحات محصول نمونه 6', N'محصول نمونه 6', 190000)
INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 410, N'توضیحات محصول نمونه 7', N'محصول نمونه 7', 200000)
INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 180, N'توضیحات محصول نمونه 8', N'محصول نمونه 8', 380000)
INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 200, N'توضیحات محصول نمونه 9', N'محصول نمونه 9', 420000)
INSERT INTO [dbo].[Products] ([IsExists], [NumberInStock], [Specification], [Title], [Price]) VALUES ( 1, 150, N'توضیحات محصول نمونه 10', N'محصول نمونه 10', 220000)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[Products]");
        }
    }
}
