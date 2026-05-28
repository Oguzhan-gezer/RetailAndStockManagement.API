using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetailAndStockManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                schema: "dbo",
                columns: table => new
                {
                    Barcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageBase64 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Barcode);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                    table.ForeignKey(
                        name: "FK_Regions_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "dbo",
                columns: table => new
                {
                    StoreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoreLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.StoreId);
                    table.ForeignKey(
                        name: "FK_Store_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductStore",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: false),
                    OptionCount = table.Column<int>(type: "int", nullable: false),
                    SizeXS = table.Column<int>(type: "int", nullable: false),
                    SizeS = table.Column<int>(type: "int", nullable: false),
                    SizeM = table.Column<int>(type: "int", nullable: false),
                    SizeL = table.Column<int>(type: "int", nullable: false),
                    SizeXL = table.Column<int>(type: "int", nullable: false),
                    SizeXXL = table.Column<int>(type: "int", nullable: false),
                    SizeXXXL = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductStore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductStore_Product_Barcode",
                        column: x => x.Barcode,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "Barcode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductStore_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StoreId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Store_StoreId",
                        column: x => x.StoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "TransferRequest",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SourceStoreId = table.Column<int>(type: "int", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    RemainingQuantity = table.Column<int>(type: "int", nullable: false),
                    ReqXS = table.Column<int>(type: "int", nullable: false),
                    ReqS = table.Column<int>(type: "int", nullable: false),
                    ReqM = table.Column<int>(type: "int", nullable: false),
                    ReqL = table.Column<int>(type: "int", nullable: false),
                    ReqXL = table.Column<int>(type: "int", nullable: false),
                    ReqXXL = table.Column<int>(type: "int", nullable: false),
                    ReqXXXL = table.Column<int>(type: "int", nullable: false),
                    RemXS = table.Column<int>(type: "int", nullable: false),
                    RemS = table.Column<int>(type: "int", nullable: false),
                    RemM = table.Column<int>(type: "int", nullable: false),
                    RemL = table.Column<int>(type: "int", nullable: false),
                    RemXL = table.Column<int>(type: "int", nullable: false),
                    RemXXL = table.Column<int>(type: "int", nullable: false),
                    RemXXXL = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferRequest_Product_Barcode",
                        column: x => x.Barcode,
                        principalSchema: "dbo",
                        principalTable: "Product",
                        principalColumn: "Barcode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferRequest_Store_SourceStoreId",
                        column: x => x.SourceStoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferRequest_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransferOrder",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransferRequestId = table.Column<int>(type: "int", nullable: false),
                    TargetStoreId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    QtyXS = table.Column<int>(type: "int", nullable: false),
                    QtyS = table.Column<int>(type: "int", nullable: false),
                    QtyM = table.Column<int>(type: "int", nullable: false),
                    QtyL = table.Column<int>(type: "int", nullable: false),
                    QtyXL = table.Column<int>(type: "int", nullable: false),
                    QtyXXL = table.Column<int>(type: "int", nullable: false),
                    QtyXXXL = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferOrder_Store_TargetStoreId",
                        column: x => x.TargetStoreId,
                        principalSchema: "dbo",
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrder_TransferRequest_TransferRequestId",
                        column: x => x.TransferRequestId,
                        principalSchema: "dbo",
                        principalTable: "TransferRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransferOrder_User_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalSchema: "dbo",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductStore_Barcode",
                schema: "dbo",
                table: "ProductStore",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStore_StoreId",
                schema: "dbo",
                table: "ProductStore",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Store_RegionId",
                schema: "dbo",
                table: "Store",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrder_CreatedByUserId",
                schema: "dbo",
                table: "TransferOrder",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrder_TargetStoreId",
                schema: "dbo",
                table: "TransferOrder",
                column: "TargetStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOrder_TransferRequestId",
                schema: "dbo",
                table: "TransferOrder",
                column: "TransferRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequest_Barcode",
                schema: "dbo",
                table: "TransferRequest",
                column: "Barcode");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequest_CreatedByUserId",
                schema: "dbo",
                table: "TransferRequest",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferRequest_SourceStoreId",
                schema: "dbo",
                table: "TransferRequest",
                column: "SourceStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_User_StoreId",
                schema: "dbo",
                table: "User",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                schema: "dbo",
                table: "User",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductStore",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TransferOrder",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TransferRequest",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Product",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
