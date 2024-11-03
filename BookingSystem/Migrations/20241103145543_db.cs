using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblCountry",
                columns: table => new
                {
                    countryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    countryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCountry", x => x.countryId);
                });

            migrationBuilder.CreateTable(
                name: "tblStatus",
                columns: table => new
                {
                    statusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    statusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStatus", x => x.statusId);
                });

            migrationBuilder.CreateTable(
                name: "tblClass",
                columns: table => new
                {
                    classId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    className = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    countryId = table.Column<int>(type: "int", nullable: false),
                    countryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    creditToBuy = table.Column<int>(type: "int", nullable: true),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    classDuration = table.Column<int>(type: "int", nullable: true),
                    maxSlots = table.Column<int>(type: "int", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblClass", x => x.classId);
                    table.ForeignKey(
                        name: "FK_tblClass_tblCountry_countryId",
                        column: x => x.countryId,
                        principalTable: "tblCountry",
                        principalColumn: "countryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPackage",
                columns: table => new
                {
                    packageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    packageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    countryId = table.Column<int>(type: "int", nullable: true),
                    countryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    credits = table.Column<int>(type: "int", nullable: true),
                    packagePrice = table.Column<decimal>(type: "decimal(18,4)", nullable: true),
                    packageDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    expiryDays = table.Column<int>(type: "int", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPackage", x => x.packageId);
                    table.ForeignKey(
                        name: "FK_tblPackage_tblCountry_countryId",
                        column: x => x.countryId,
                        principalTable: "tblCountry",
                        principalColumn: "countryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    verifyPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    countryId = table.Column<int>(type: "int", nullable: true),
                    countryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isVarify = table.Column<bool>(type: "bit", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.userId);
                    table.ForeignKey(
                        name: "FK_tblUser_tblCountry_countryId",
                        column: x => x.countryId,
                        principalTable: "tblCountry",
                        principalColumn: "countryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblUserPackage",
                columns: table => new
                {
                    upId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    packageId = table.Column<int>(type: "int", nullable: false),
                    packagename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    creditRemain = table.Column<int>(type: "int", nullable: true),
                    isExpire = table.Column<bool>(type: "bit", nullable: false),
                    purchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    packcageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUserPackage", x => x.upId);
                    table.ForeignKey(
                        name: "FK_tblUserPackage_tblPackage_packageId",
                        column: x => x.packageId,
                        principalTable: "tblPackage",
                        principalColumn: "packageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblUserPackage_tblUser_userId",
                        column: x => x.userId,
                        principalTable: "tblUser",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblBooking",
                columns: table => new
                {
                    bookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    classId = table.Column<int>(type: "int", nullable: false),
                    className = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    statusId = table.Column<int>(type: "int", nullable: false),
                    statusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    upId = table.Column<int>(type: "int", nullable: false),
                    totalHour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    bookedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    cancelAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ssCheckedIn = table.Column<bool>(type: "bit", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblBooking", x => x.bookingId);
                    table.ForeignKey(
                        name: "FK_tblBooking_tblClass_classId",
                        column: x => x.classId,
                        principalTable: "tblClass",
                        principalColumn: "classId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblBooking_tblStatus_statusId",
                        column: x => x.statusId,
                        principalTable: "tblStatus",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblBooking_tblUserPackage_upId",
                        column: x => x.upId,
                        principalTable: "tblUserPackage",
                        principalColumn: "upId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblBooking_tblUser_userId",
                        column: x => x.userId,
                        principalTable: "tblUser",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTransactionHistory",
                columns: table => new
                {
                    thId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    upId = table.Column<int>(type: "int", nullable: false),
                    classId = table.Column<int>(type: "int", nullable: false),
                    className = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    usedCredits = table.Column<int>(type: "int", nullable: false),
                    refundCredits = table.Column<int>(type: "int", nullable: false),
                    transactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTransactionHistory", x => x.thId);
                    table.ForeignKey(
                        name: "FK_tblTransactionHistory_tblClass_classId",
                        column: x => x.classId,
                        principalTable: "tblClass",
                        principalColumn: "classId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblTransactionHistory_tblUserPackage_upId",
                        column: x => x.upId,
                        principalTable: "tblUserPackage",
                        principalColumn: "upId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblWaitlist",
                columns: table => new
                {
                    wailistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bookingId = table.Column<int>(type: "int", nullable: false),
                    classId = table.Column<int>(type: "int", nullable: false),
                    statusId = table.Column<int>(type: "int", nullable: false),
                    statusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblWaitlist", x => x.wailistId);
                    table.ForeignKey(
                        name: "FK_tblWaitlist_tblBooking_bookingId",
                        column: x => x.bookingId,
                        principalTable: "tblBooking",
                        principalColumn: "bookingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWaitlist_tblClass_classId",
                        column: x => x.classId,
                        principalTable: "tblClass",
                        principalColumn: "classId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblWaitlist_tblStatus_statusId",
                        column: x => x.statusId,
                        principalTable: "tblStatus",
                        principalColumn: "statusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserId",
                table: "tblBooking",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBooking_classId",
                table: "tblBooking",
                column: "classId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBooking_statusId",
                table: "tblBooking",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_tblBooking_upId",
                table: "tblBooking",
                column: "upId");

            migrationBuilder.CreateIndex(
                name: "IX_Class_CountryId",
                table: "tblClass",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_Package_CountryId",
                table: "tblPackage",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTransactionHistory_classId",
                table: "tblTransactionHistory",
                column: "classId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionHistory_UserPackageId",
                table: "tblTransactionHistory",
                column: "upId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CountryId",
                table: "tblUser",
                column: "countryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblUserPackage_packageId",
                table: "tblUserPackage",
                column: "packageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPackage_UserId",
                table: "tblUserPackage",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWaitlist_classId",
                table: "tblWaitlist",
                column: "classId");

            migrationBuilder.CreateIndex(
                name: "IX_tblWaitlist_statusId",
                table: "tblWaitlist",
                column: "statusId");

            migrationBuilder.CreateIndex(
                name: "IX_Waitlist_BookingId",
                table: "tblWaitlist",
                column: "bookingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblTransactionHistory");

            migrationBuilder.DropTable(
                name: "tblWaitlist");

            migrationBuilder.DropTable(
                name: "tblBooking");

            migrationBuilder.DropTable(
                name: "tblClass");

            migrationBuilder.DropTable(
                name: "tblStatus");

            migrationBuilder.DropTable(
                name: "tblUserPackage");

            migrationBuilder.DropTable(
                name: "tblPackage");

            migrationBuilder.DropTable(
                name: "tblUser");

            migrationBuilder.DropTable(
                name: "tblCountry");
        }
    }
}
