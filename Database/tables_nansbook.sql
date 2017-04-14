use techstar_nansbook
go

-- Remove all foreign keys
print 'Remove all foreign keys'
while(exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_TYPE='FOREIGN KEY'))
begin
	declare @sql nvarchar(2000)
	SELECT TOP 1 @sql=('ALTER TABLE ' + TABLE_SCHEMA + '.[' + TABLE_NAME
	+ '] DROP CONSTRAINT [' + CONSTRAINT_NAME + ']')
	FROM information_schema.table_constraints
	WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'
	exec (@sql)
end

-- Table: Store
print 'Table: Store'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Store')
begin
	drop table dbo.Store
end
go
create table dbo.Store
(
	Id int not null identity(1,1) primary key,
	Name nvarchar(100) not null,
	[Description] text null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go
insert into Store values('Independent', 'No store affiliation. User is using app for personal bookkeeping', 'system', GETDATE(), 'system', GETDATE())
insert into Store values('Dave''s Pawn Shop', null, 'system', GETDATE(), 'system', GETDATE())
insert into Store values('Paul''s Groceries', null, 'system', GETDATE(), 'system', GETDATE())
go

---------------------------------------------------------------------------------------------------------
-- START OF USER INFO TABLES
---------------------------------------------------------------------------------------------------------

-- Table: State
print 'Table: State'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'State')
begin
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserProfile_State')
	begin
		alter table dbo.UserProfile drop constraint fk_UserProfile_State
	end
	
	drop table dbo.[State]
end
go
create table dbo.[State]
(
	Id int not null identity(1,1) primary key,
	Abbrev nvarchar(5) not null,
	Name nvarchar(100) not null
)
go
insert into dbo.[State] values('AK', 'Alaska')
insert into dbo.[State] values('AL', 'Alabama')
insert into dbo.[State] values('AR', 'Arkansas')
insert into dbo.[State] values('AZ', 'Arizona')
insert into dbo.[State] values('CA', 'California')
insert into dbo.[State] values('CO', 'Colorado')
insert into dbo.[State] values('CT', 'Connecticut')
insert into dbo.[State] values('DC', 'District Of Columbia')
insert into dbo.[State] values('DE', 'Delaware')
insert into dbo.[State] values('FL', 'Florida')
insert into dbo.[State] values('GA', 'Georgia')
insert into dbo.[State] values('HI', 'Hawaii')
insert into dbo.[State] values('IA', 'Iowa')
insert into dbo.[State] values('ID', 'Idaho')
insert into dbo.[State] values('IL', 'Illinois')
insert into dbo.[State] values('IN', 'Indiana')
insert into dbo.[State] values('KS', 'Kansas')
insert into dbo.[State] values('KY', 'Kentucky')
insert into dbo.[State] values('LA', 'Louisiana')
insert into dbo.[State] values('MA', 'Massachusetts')
insert into dbo.[State] values('MD', 'Maryland')
insert into dbo.[State] values('ME', 'Maine')
insert into dbo.[State] values('MI', 'Michigan')
insert into dbo.[State] values('MN', 'Minnesota')
insert into dbo.[State] values('MO', 'Missouri')
insert into dbo.[State] values('MS', 'Mississippi')
insert into dbo.[State] values('MT', 'Montana')
insert into dbo.[State] values('NC', 'North Carolina')
insert into dbo.[State] values('ND', 'North Dakota')
insert into dbo.[State] values('NE', 'Nebraska')
insert into dbo.[State] values('NH', 'New Hampshire')
insert into dbo.[State] values('NJ', 'New Jersey')
insert into dbo.[State] values('NM', 'New Mexico')
insert into dbo.[State] values('NV', 'Nevada')
insert into dbo.[State] values('NY', 'New York')
insert into dbo.[State] values('OH', 'Ohio')
insert into dbo.[State] values('OK', 'Oklahoma')
insert into dbo.[State] values('OR', 'Oregon')
insert into dbo.[State] values('PA', 'Pennsylvania')
insert into dbo.[State] values('RI', 'Rhode Island')
insert into dbo.[State] values('SC', 'South Carolina')
insert into dbo.[State] values('SD', 'South Dakota')
insert into dbo.[State] values('TN', 'Tennessee')
insert into dbo.[State] values('TX', 'Texas')
insert into dbo.[State] values('UT', 'Utah')
insert into dbo.[State] values('VA', 'Virginia')
insert into dbo.[State] values('VT', 'Vermont')
insert into dbo.[State] values('WA', 'Washington')
insert into dbo.[State] values('WI', 'Wisconsin')
insert into dbo.[State] values('WV', 'West Virginia')
insert into dbo.[State] values('WY', 'Wyoming')
go

-- Table: SecurityQuestion
print 'Table: SecurityQuestion'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'SecurityQuestion')
begin
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserProfile_SecurityQuestion1')
	begin
		alter table dbo.UserProfile drop constraint fk_UserProfile_SecurityQuestion1
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserProfile_SecurityQuestion2')
	begin
		alter table dbo.UserProfile drop constraint fk_UserProfile_SecurityQuestion2
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserProfile_SecurityQuestion3')
	begin
		alter table dbo.UserProfile drop constraint fk_UserProfile_SecurityQuestion3
	end

	drop table dbo.SecurityQuestion
end
go
create table dbo.SecurityQuestion
(
	Id int not null identity(1,1) primary key,
	Question nvarchar(250) not null
)
go
insert into dbo.SecurityQuestion values('What is your favorite color?')
insert into dbo.SecurityQuestion values('What is your mother''s maiden name?')
insert into dbo.SecurityQuestion values('What is the name of the street you grew up on?')
insert into dbo.SecurityQuestion values('What is your dog''s name?')
insert into dbo.SecurityQuestion values('What is your high school mascot?')
insert into dbo.SecurityQuestion values('What is childhood best friend''s name?')
go

-- Table: UserProfile
print 'Table: UserProfile'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'UserProfile')
begin	
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_UserId')
	begin
		alter table dbo.webpages_UsersInRoles drop constraint fk_UserId
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_SalesEntry_Technician')
	begin
		alter table dbo.SalesEntry drop constraint fk_SalesEntry_Technician
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_SalesEntry_CreatedBy')
	begin
		alter table dbo.SalesEntry drop constraint fk_SalesEntry_CreatedBy
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_SalesEntry_UpdatedBy')
	begin
		alter table dbo.SalesEntry drop constraint fk_SalesEntry_UpdatedBy
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_Category_CreatedBy')
	begin
		alter table dbo.Category drop constraint fk_Category_CreatedBy
	end
	if exists(select 1 from INFORMATION_SCHEMA.TABLE_CONSTRAINTS where CONSTRAINT_NAME = 'fk_Category_UpdatedBy')
	begin
		alter table dbo.Category drop constraint fk_Category_UpdatedBy
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
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'webpages_Membership')
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
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'webpages_OAuthMembership')
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
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'webpages_Roles')
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
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'webpages_UsersInRoles')
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
insert into dbo.userprofile values('gilbert.sauceda@gmail.com','Gilbert',NULL,'Sauceda',NULL,NULL,NULL,NULL,44,NULL,NULL,'6efa18e7-a6ba-4f58-b9f0-d66702765752.jpg',NULL,1,2,3,'blue','deluna','pike')
insert into dbo.webpages_Membership values(1,'2016-05-09 00:45:55.853','1kLoolPBj1Bz57CjDlsImQ2',1,NULL,0,'ADVgj5si1FRYI1xGDOVFEAVuW8r4kMg/UYZiwXxyr554HVh+uqm7wjYOYEP1PiIKsQ==','2016-05-09 00:45:55.853','',NULL,NULL)
insert into dbo.webpages_usersinroles values(1,1)

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

-- Table: UserXStore
print 'Table: UserXStore'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'UserXStore')
begin
	drop table dbo.UserXStore
end
go
create table dbo.UserXStore
(
	Id int not null identity(1,1) primary key,
	UserId int not null constraint fk_UserXStore_UserProfile foreign key references dbo.UserProfile(UserId),
	StoreId int not null constraint fk_UserXStore_Store foreign key references dbo.Store(Id),
	[Current] bit not null default 1
)
go

insert into UserXStore values(1,1,1)
insert into UserXStore values(1,2,0)
insert into UserXStore values(1,3,0)
go
-- select * from userxstore
---------------------------------------------------------------------------------------------------------
-- START OF LOOKUP TABLES
---------------------------------------------------------------------------------------------------------

-- Table: Service
print 'Table: Service'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Service')
begin
	drop table dbo.[Service]
end
go
create table dbo.[Service]
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_Service_UserXStore foreign key references dbo.UserXStore(Id),
	ParentId int null constraint fk_Parent_Service foreign key references dbo.[Service](Id),
	Name nvarchar(100) not null,
	Price money null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: Product
print 'Table: Product'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Product')
begin
	drop table dbo.Product
end
go
create table dbo.Product
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_Product_UserXStore foreign key references dbo.UserXStore(Id),
	ParentId int null constraint fk_Parent_Product foreign key references dbo.Product(Id),
	Name nvarchar(100) not null,
	Price money null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: GiftCard
print 'Table: GiftCard'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'GiftCard')
begin
	drop table dbo.GiftCard
end
go
create table dbo.GiftCard
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_GiftCard_UserXStore foreign key references dbo.UserXStore(Id),
	Name nvarchar(100) not null,
	CreditAmount money not null,
	ExpirationDate datetime null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: Discount
print 'Table: Discount'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Discount')
begin
	drop table dbo.Discount
end
go
create table dbo.Discount
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_Discount_UserXStore foreign key references dbo.UserXStore(Id),
	ParentId int null constraint fk_Parent_Discount foreign key references dbo.Discount(Id),
	ProductId int null constraint fk_Discount_Product foreign key references dbo.Product(Id),
	ServiceId int null constraint fk_Discount_Service foreign key references dbo.[Service](Id),
	Name nvarchar(50) not null,
	Amount money null,
	Percentage int null,
	ExpirationDate datetime null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: PaymentMethod
print 'Table: PaymentMethod'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'PaymentMethod')
begin
	drop table dbo.PaymentMethod
end
go
create table dbo.PaymentMethod
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_PaymentMethod_UserXStore foreign key references dbo.UserXStore(Id),
	Name nvarchar(100) not null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: ExpenseCategory
print 'Table: ExpenseCategory'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'ExpenseCategory')
begin
	drop table dbo.ExpenseCategory
end
go
create table dbo.ExpenseCategory
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_ExpenseCategory_UserXStore foreign key references dbo.UserXStore(Id),
	ParentId int null constraint fk_Parent_ExpenseCategory foreign key references dbo.ExpenseCategory(Id),
	Name nvarchar(100) not null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: CommissionRate
print 'Table: CommissionRate'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'CommissionRate')
begin
	drop table dbo.CommissionRate
end
go
create table dbo.CommissionRate
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_CommissionRate_UserXStore foreign key references dbo.UserXStore(Id),
	StoreCommPct int not null,
	StoreCashPct int not null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: Salary
print 'Table: Salary'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Salary')
begin
	drop table dbo.Salary
end
go
create table dbo.Salary
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_Salary_UserXStore foreign key references dbo.UserXStore(Id),
	Amount money not null,
	ExpirationDate datetime null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: Deduction
print 'Table: Deduction'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Deduction')
begin
	drop table dbo.Deduction
end
go
create table dbo.Deduction
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_Deduction_UserXStore foreign key references dbo.UserXStore(Id),
	FixedAmtPerSvcSale money null,
	PctPerSvcSale int null,
	PctTotAllSvcSales int null,
	DailyAmt money null,
	DailyCleaningExp money null,
	DailyCleaningExpWkday int null,
	TipProcessingPct int null,	
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: AppSetting
print 'Table: AppSetting'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'AppSetting')
begin
	drop table dbo.AppSetting
end
go
create table dbo.AppSetting
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_AppSetting_UserXStore foreign key references dbo.UserXStore(Id),
	NotifyByEmailForInvites bit not null,
	NotifyByEmailForPaychecks bit not null,
	NotifyInAppForInvites bit not null,
	NotifyInAppForPaychecks bit not null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: Notification
print 'Table: Notification'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Notification')
begin
	drop table dbo.[Notification]
end
go
create table dbo.[Notification]
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_Notification_UserXStore foreign key references dbo.UserXStore(Id),
	[Message] nvarchar(250) not null,
	Link nvarchar(250) null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: Paycheck
print 'Table: Paycheck'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Paycheck')
begin
	drop table dbo.Paycheck
end
go
create table dbo.Paycheck
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_Paycheck_UserXStore foreign key references dbo.UserXStore(Id),
	CheckNumber nvarchar(50) not null,
	CommissionInCheck money not null,
	TipAmount money not null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

---------------------------------------------------------------------------------------------------------
-- START OF DATA ENTRY TABLES
---------------------------------------------------------------------------------------------------------

-- Table: Sale
print 'Table: Sale'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Sale')
begin
	drop table dbo.Sale
end
go
create table dbo.Sale
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_Sale_UserXStore foreign key references dbo.UserXStore(Id),
	ProductId int null constraint fk_Sale_Product foreign key references dbo.Product(Id),
	ServiceId int null constraint fk_Sale_Service foreign key references dbo.[Service](Id),
	SalePaymentMethodId int not null constraint fk_Sale_PaymentMethod foreign key references dbo.PaymentMethod(Id),
	SaleAmount money not null,
	TipPaymentMethodId int null constraint fk_Tip_PaymentMethod foreign key references dbo.PaymentMethod(Id),
	TipAmount money null,
	DeductionAmount money null,
	DiscountAmount money null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go

-- Table: Expense
print 'Table: Expense'
if exists(select 1 from INFORMATION_SCHEMA.TABLES where TABLE_NAME = 'Expense')
begin
	drop table dbo.Expense
end
go
create table dbo.Expense
(
	Id int not null identity(1,1) primary key,
	UserXStoreId int not null constraint fk_Expense_UserXStore foreign key references dbo.UserXStore(Id),
	ExpenseCategoryId int not null constraint fk_Expense_ExpenseCategory foreign key references dbo.ExpenseCategory(Id),
	PaymentMethodId int not null constraint fk_Expense_PaymentMethod foreign key references dbo.PaymentMethod(Id),
	Amount money not null,
	[Description] text null,
	Vendor nvarchar(50) null,
	CreatedBy nvarchar(50) not null,
	CreatedDate datetime not null default getdate(),
	UpdatedBy nvarchar(50) not null,
	UpdatedDate datetime not null default getdate()
)
go