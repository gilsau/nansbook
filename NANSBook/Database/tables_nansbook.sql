use techstar_nansbook
go

-- sp_tables
-- select * from userprofile
-- select * from webpages_Membership

-- Table: State
print 'Table: State'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'State')
begin
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserProfile_State')
	begin
		alter table UserProfile drop constraint fk_UserProfile_State
	end
	
	drop table [State]
end
go
create table [State]
(
	Id int not null identity(1,1) primary key,
	Abbrev nvarchar(5) not null,
	Name nvarchar(100) not null
)
go
insert into [State] values('AK', 'Alaska')
insert into [State] values('AL', 'Alabama')
insert into [State] values('AR', 'Arkansas')
insert into [State] values('AZ', 'Arizona')
insert into [State] values('CA', 'California')
insert into [State] values('CO', 'Colorado')
insert into [State] values('CT', 'Connecticut')
insert into [State] values('DC', 'District Of Columbia')
insert into [State] values('DE', 'Delaware')
insert into [State] values('FL', 'Florida')
insert into [State] values('GA', 'Georgia')
insert into [State] values('HI', 'Hawaii')
insert into [State] values('IA', 'Iowa')
insert into [State] values('ID', 'Idaho')
insert into [State] values('IL', 'Illinois')
insert into [State] values('IN', 'Indiana')
insert into [State] values('KS', 'Kansas')
insert into [State] values('KY', 'Kentucky')
insert into [State] values('LA', 'Louisiana')
insert into [State] values('MA', 'Massachusetts')
insert into [State] values('MD', 'Maryland')
insert into [State] values('ME', 'Maine')
insert into [State] values('MI', 'Michigan')
insert into [State] values('MN', 'Minnesota')
insert into [State] values('MO', 'Missouri')
insert into [State] values('MS', 'Mississippi')
insert into [State] values('MT', 'Montana')
insert into [State] values('NC', 'North Carolina')
insert into [State] values('ND', 'North Dakota')
insert into [State] values('NE', 'Nebraska')
insert into [State] values('NH', 'New Hampshire')
insert into [State] values('NJ', 'New Jersey')
insert into [State] values('NM', 'New Mexico')
insert into [State] values('NV', 'Nevada')
insert into [State] values('NY', 'New York')
insert into [State] values('OH', 'Ohio')
insert into [State] values('OK', 'Oklahoma')
insert into [State] values('OR', 'Oregon')
insert into [State] values('PA', 'Pennsylvania')
insert into [State] values('RI', 'Rhode Island')
insert into [State] values('SC', 'South Carolina')
insert into [State] values('SD', 'South Dakota')
insert into [State] values('TN', 'Tennessee')
insert into [State] values('TX', 'Texas')
insert into [State] values('UT', 'Utah')
insert into [State] values('VA', 'Virginia')
insert into [State] values('VT', 'Vermont')
insert into [State] values('WA', 'Washington')
insert into [State] values('WI', 'Wisconsin')
insert into [State] values('WV', 'West Virginia')
insert into [State] values('WY', 'Wyoming')
go

-- Table: SecurityQuestion
print 'Table: SecurityQuestion'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'SecurityQuestion')
begin
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserProfile_SecurityQuestion1')
	begin
		alter table UserProfile drop constraint fk_UserProfile_SecurityQuestion1
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserProfile_SecurityQuestion2')
	begin
		alter table UserProfile drop constraint fk_UserProfile_SecurityQuestion2
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserProfile_SecurityQuestion3')
	begin
		alter table UserProfile drop constraint fk_UserProfile_SecurityQuestion3
	end

	drop table SecurityQuestion
end
go
create table SecurityQuestion
(
	Id int not null identity(1,1) primary key,
	Question nvarchar(250) not null
)
go
insert into SecurityQuestion values('What is your favorite color?')
insert into SecurityQuestion values('What is your mother''s maiden name?')
insert into SecurityQuestion values('What is the name of the street you grew up on?')
insert into SecurityQuestion values('What is your dog''s name?')
insert into SecurityQuestion values('What is your high school mascot?')
insert into SecurityQuestion values('What is childhood best friend''s name?')
go


-- Table: UserProfile
print 'Table: UserProfile'
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND type in (N'U'))
begin	
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserId')
	begin
		alter table webpages_UsersInRoles drop constraint fk_UserId
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_SalesEntry_Technician')
	begin
		alter table SalesEntry drop constraint fk_SalesEntry_Technician
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_SalesEntry_CreatedBy')
	begin
		alter table SalesEntry drop constraint fk_SalesEntry_CreatedBy
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_SalesEntry_UpdatedBy')
	begin
		alter table SalesEntry drop constraint fk_SalesEntry_UpdatedBy
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_Category_CreatedBy')
	begin
		alter table Category drop constraint fk_Category_CreatedBy
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_Category_UpdatedBy')
	begin
		alter table Category drop constraint fk_Category_UpdatedBy
	end
	
	DROP TABLE [dbo].[UserProfile]
end
GO
CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](56) NOT NULL,
	FirstName nvarchar(100) not null,
	MiddleName nvarchar(100) null,
	LastName nvarchar(100) not null,
	NickName nvarchar(100) null,
	Address1 nvarchar(100) null,
	Address2 nvarchar(100) null,
	City nvarchar(50) null,
	[State] int null constraint fk_UserProfile_State foreign key references [State](Id),
	HomePhone nvarchar(15) null,
	MobilePhone nvarchar(15) null,
	PhotoFileName nvarchar(250) null,
	SocialSecurity nvarchar(15) null,
	SecQuestion1 int null constraint fk_UserProfile_SecurityQuestion1 foreign key references SecurityQuestion(Id),
	SecQuestion2 int null constraint fk_UserProfile_SecurityQuestion2 foreign key references SecurityQuestion(Id),
	SecQuestion3 int null constraint fk_UserProfile_SecurityQuestion3 foreign key references SecurityQuestion(Id),
	SecAnswer1 nvarchar(100) null,
	SecAnswer2 nvarchar(100) null,
	SecAnswer3 nvarchar(100) null
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
go

-- Table: webpages_Membership
print 'Table: webpages_Membership'
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_Membership]') AND type in (N'U'))
DROP TABLE [dbo].[webpages_Membership]
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO

ALTER TABLE [dbo].[webpages_Membership] ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO

--  Table: webpages_OAuthMembership
print 'Table: webpages_OAuthMembership'
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_OAuthMembership]') AND type in (N'U'))
DROP TABLE [dbo].[webpages_OAuthMembership]
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

--  Table: webpages_Roles
print 'Table: webpages_Roles'
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_Roles]') AND type in (N'U'))
begin	
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_RoleId')
	begin
		alter table webpages_UsersInRoles drop constraint fk_RoleId
	end
	
	DROP TABLE [dbo].[webpages_Roles]
end
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
insert into webpages_Roles values('Admin')
insert into webpages_Roles values('Manager')
insert into webpages_Roles values('Technician')
go

--  Table: webpages_UsersInRoles
print 'Table: webpages_UsersInRoles'
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_UsersInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[webpages_UsersInRoles]
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO

ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_RoleId]
GO

ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [fk_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO

ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [fk_UserId]
GO

-- Add Admin
insert into userprofile values('gilbert.sauceda@gmail.com','Gilbert',NULL,'Sauceda',NULL,NULL,NULL,NULL,44,NULL,NULL,'6efa18e7-a6ba-4f58-b9f0-d66702765752.jpg',NULL,1,2,3,'blue','deluna','pike')
insert into webpages_Membership values(1,'2016-05-09 00:45:55.853','1kLoolPBj1Bz57CjDlsImQ2',1,NULL,0,'ADVgj5si1FRYI1xGDOVFEAVuW8r4kMg/UYZiwXxyr554HVh+uqm7wjYOYEP1PiIKsQ==','2016-05-09 00:45:55.853','',NULL,NULL)
insert into webpages_usersinroles values(1,1)
go

-- Table: CategoryType
print 'Table: CategoryType'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'CategoryType')
begin
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_Category_CategoryType')
	begin
		alter table Category drop constraint fk_Category_CategoryType
	end

	drop table CategoryType
end
go
create table CategoryType
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(100) not null
)
go
insert into CategoryType values('Expense')
insert into CategoryType values('Service')
insert into CategoryType values('Payment Method')
insert into CategoryType values('Discount')
insert into CategoryType values('Sales')
insert into CategoryType values('Product')
go
	
-- Table: Category
print 'Table: Category'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Category')
begin
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_SalesEntry_Discount')
	begin
		alter table SalesEntry drop constraint fk_SalesEntry_Discount
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_SalesEntry_Service')
	begin
		alter table SalesEntry drop constraint fk_SalesEntry_Service
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_SalesEntry_Product')
	begin
		alter table SalesEntry drop constraint fk_SalesEntry_Product
	end

	drop table Category
end
go
create table Category
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(100) not null,
	ParentId int null constraint fk_Category_Category foreign key references Category(Id),
	CategoryType int not null constraint fk_Category_CategoryType foreign key references CategoryType(Id),
	[Description] text null,
	Price money null,
	Percentage int null,
	ServiceId int null constraint fk_Category_Service foreign key references Category(Id),
	ProductId int null constraint fk_Category_Product foreign key references Category(Id),
	CreatedById int not null constraint fk_Category_CreatedBy foreign key references userprofile(UserId),
	CreatedDate datetime not null default getdate(),
	UpdatedById int not null constraint fk_Category_UpdatedBy foreign key references userprofile(UserId),
	UpdatedDate datetime not null default getdate()
)
go
insert into Category values('Advertising', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Bank Fee', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Credit Card Processing Fee', 2, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Checking Account Fee', 2, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Saving Account Fee', 2, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Car & Truck Expenses', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Commission & Fees', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Contract Labor', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Depletion', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Depreciation', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Donation', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Employee Benefit Program', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Insurance', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('InterestsLegal & Professional Fee', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Office Expense', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Personal Expense', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Rent or Lease', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Repair & Maintenance', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Supplies', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Taxes & Licenses', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Travel, Meals & Entertainment', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Travel Expense', 21, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Meal Expense', 21, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Entertainment Expense', 21, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Utilities', null, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Telephone', 25, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Internet', 25, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Electricity', 25, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Gas', 25, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Water', 25, 1, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())

insert into Category values('Manicure', null, 2, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Pedicure', null, 2, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Nails', null, 2, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Facial', null, 2, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Waxing', null, 2, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Massage', null, 2, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())

insert into Category values('Cash', null, 3, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Check', null, 3, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Credit Card', null, 3, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Bank Card', null, 3, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Online Banking', null, 3, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Gift Card', null, 3, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())

insert into Category values('Internet', null, 4, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Mails', null, 4, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Coupons', null, 4, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Store Specials', null, 4, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())

insert into Category values('Products', null, 5, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Services', null, 5, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Tips', null, 5, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())

insert into Category values('Shampoo 1', null, 6, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Gel 1', null, 6, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Hair Spray 1', null, 6, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Shampoo 2', null, 6, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Gel 2', null, 6, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())
insert into Category values('Hair Spray 2', null, 6, null, null, null, null, null, 1, GETDATE(), 1, GETDATE())

go

-- Table: DataEntryType
print 'Table: DataEntryType'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'DataEntryType')
begin
	drop table DataEntryType
end
go
create table DataEntryType
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(100) not null
)
go
insert into DataEntryType values('Store Sales')
insert into DataEntryType values('Individual Sales')
insert into DataEntryType values('Store Expense')
insert into DataEntryType values('Individual Expense')
go

-- Table: Sales Entry
print 'Table: SalesEntry'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'SalesEntry')
begin
	drop table SalesEntry
end
go
create table SalesEntry
(
	Id int not null identity(1,1) primary key,
	TechnicianId int not null constraint fk_SalesEntry_Technician foreign key references userprofile(UserId),
	ServiceId int null constraint fk_SalesEntry_Service foreign key references Category(Id),
	ProductId int null constraint fk_SalesEntry_Product foreign key references Category(Id),
	SaleAmt money not null,
	TipAmt money null,
	DeductionAmt money null,
	DiscountCategoryId int null constraint fk_SalesEntry_Discount foreign key references Category(Id),
	TimeOfSale datetime not null default getdate(),
	CreatedById int not null constraint fk_SalesEntry_CreatedBy foreign key references userprofile(UserId),
	CreatedDate datetime not null default getdate(),
	UpdatedById int not null constraint fk_SalesEntry_UpdatedBy foreign key references userprofile(UserId),
	UpdatedDate datetime not null default getdate()
)
go


-- Load table var with users, loop thru them, and enter them into userprofile table
print 'ADDING USERS'
declare @RowsToProcess int, @CurrentRow int, @userEmail nvarchar(50), @userFirst nvarchar(50), @userLast nvarchar(50), @userPic nvarchar(100), @userId int
declare @tblUsers table(id int not null primary key identity(1,1), email nvarchar(50) not null, firstname nvarchar(50) not null, lastname nvarchar(50) not null, profilepic nvarchar(100) not null)

insert into @tblUsers values('tom.shears@gmail.com', 'Tom', 'Shears', '8c2bbf1d-8691-43f2-93ae-2436f8f64692.jpg')
insert into @tblUsers values('karen.spell@gmail.com', 'Karen', 'Spell', 'ed00a065-7d95-4247-9c14-161cdf04a3cc.jpg')
insert into @tblUsers values('charlie.fond@gmail.com', 'Charlie', 'Fond', 'ade52c04-b97b-4eac-9963-d11541ce3c44.jpg')
insert into @tblUsers values('zach.flores@gmail.com', 'Zach', 'Flores', 'd101ca88-b570-41ab-8452-8ffcd429c2be.jpg')
insert into @tblUsers values('sheila.kind@gmail.com', 'Sheila', 'Kind', 'fce323b5-2746-4afd-9cc4-05dd58462bc2.jpg')

SET @RowsToProcess=5
print '@RowsToProcess: ' + cast(@RowsToProcess as varchar(10))
SET @CurrentRow=0
WHILE @CurrentRow<@RowsToProcess
BEGIN
    SET @CurrentRow=@CurrentRow+1
    SELECT 
		@userId=id+1,
        @userEmail=email,
        @userFirst=firstname,
        @userLast=lastname,
        @userPic=profilepic
        FROM @tblUsers
        WHERE id=@CurrentRow

	print '@userId: ' + cast(@userId as nvarchar(50))
	print '@userEmail: ' + @userEmail
	print '@userFirst: ' + @userFirst
	print '@userLast: ' + @userLast
	print '@userPic: ' + @userPic

    insert into userprofile values(@userEmail,@userFirst,NULL,@userLast,NULL,NULL,NULL,NULL,44,NULL,NULL,@userPic,NULL,1,2,3,'blue','deluna','pike')
	insert into webpages_Membership values(@userId,'2016-05-09 00:45:55.853','1kLoolPBj1Bz57CjDlsImQ2',1,NULL,0,'ADVgj5si1FRYI1xGDOVFEAVuW8r4kMg/UYZiwXxyr554HVh+uqm7wjYOYEP1PiIKsQ==','2016-05-09 00:45:55.853','',NULL,NULL)
	insert into webpages_usersinroles values(@userId,3)
END
go


/*
sp_tables

sp_columns userprofile
select * from state
select * from userprofile

select * from webpages_membership

select * from webpages_UsersInRoles

select * from webpages_Roles

update webpages_membership set isconfirmed = 1

*/














