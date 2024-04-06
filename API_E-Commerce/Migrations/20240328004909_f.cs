using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API_E_Commerce.Migrations
{
    public partial class f : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "ProfilePictureUrl",
            table: "AspNetUsers",
            type: "nvarchar(max)",
            nullable: true);
        }
    }
}
