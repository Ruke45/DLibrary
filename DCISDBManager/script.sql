USE [NCE_DB]
GO
/****** Object:  StoredProcedure [dbo].[ChangeUserPassword]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ChangeUserPassword](@UserId varchar(20),@Password varchar(50))
AS
BEGIN
update tblUser
set Password=@Password
where UserId=@UserId
END









GO
/****** Object:  StoredProcedure [dbo].[CreateUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateUser]
(@UserId varchar(20), 
@UserGroupID nvarchar(20),
@PersonName nvarchar(150), 
@Password nvarchar(200), 
@CreatedBy nvarchar(50),
@IsActive varchar(1),
@IsVat varchar(5)
)
AS
BEGIN
	
	insert into tblUser
	(UserId, PersonName, UserGroupID, Password, CreatedBy, CreatedDate, UpdateDate,IsActive,IsVat)
	values
	(@UserId, @PersonName, @UserGroupID, @Password, @CreatedBy, getdate(),getdate(),@IsActive,@IsVat)

END









GO
/****** Object:  StoredProcedure [dbo].[DCISApproveCustomerRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISApproveCustomerRequest] 
	-- Add the parameters for the stored procedure here
	(@RequestId varchar(20),@Status varchar(10),@ModifiedBy varchar(20),@RejectReasonCode varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblCustomerRequest SET Status=@Status,ModifiedBy=@ModifiedBy,ModifiedDate=GETDATE(),RejectReasonCode=@RejectReasonCode WHERE RequestId=@RequestId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISApproveUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISApproveUser]
	(@UserId varchar(20),@IsActive varchar(1))
AS
BEGIN
	UPDATE
		tblUser
	SET
		IsActive=@IsActive
	WHERE
		UserID=@UserId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISCertficateCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISCertficateCount](@ReqID  varchar(10))
AS
BEGIN
select(
select
	COUNT (Remark)
	from
	 tblEmailBasedCertificateRequest A,
	 tblCertificate C
	 where Remark!='C' and Status='A'and A.RequestId=C.RequestId

)+(
select

 COUNT (Remark)
	from
	 tblUploadBasedCertificateRequest A,
	 tblCertificate C
	 where Remark!='C' and Status='A' and A.RequestId=C.RequestId)
END



GO
/****** Object:  StoredProcedure [dbo].[DCISChangeUserPassword]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISChangeUserPassword](@UserId varchar(20),@Password varchar(50))
AS
BEGIN
update tblUser
set Password=@Password
where UserId=@UserId
END













GO
/****** Object:  StoredProcedure [dbo].[DCISCheckAllCustomerToEdit]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISCheckAllCustomerToEdit]
	(@customerId varchar(20),@companyName varchar(150))
AS
BEGIN
	SELECT *
	FROM
	tblCustomer
	WHERE
	CustomerId!=@customerId
	AND
	CustomerName=@companyName
END





GO
/****** Object:  StoredProcedure [dbo].[DCISCheckAuth]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISCheckAuth] 
	-- Add the parameters for the stored procedure here
	(@groupId varchar(20),@functionId varchar(10))
	AS
BEGIN
	SELECT
	*
	FROM
		tblGroupFunction
		WHERE
		GroupId=@groupId
		AND
		FunctionId=@functionId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISCheckCustomer]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISCheckCustomer] 
	(@CustomerName varchar(150),@Status varchar(1),@pending varchar(1))
AS
BEGIN
	
	
	SELECT CustomerName AS 'Name' FROM tblCustomer WHERE CustomerName=@CustomerName AND Status=@Status
	Union
	SELECT Name FROM tblCustomerRequest WHERE Name=@CustomerName AND Status=@pending
END





GO
/****** Object:  StoredProcedure [dbo].[DCISCheckLetterTable]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[DCISCheckLetterTable] 
	@CustomerId varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblCustomerRegistartionFiles WHERE CustomerId=@CustomerId
END



GO
/****** Object:  StoredProcedure [dbo].[DCISCheckPackageDescription]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISCheckPackageDescription](@PackageDescription varchar(100))
AS
BEGIN
select
PackageDescription,IsActive

from tblPackageType
where PackageDescription=@PackageDescription
and IsActive='y'

END





GO
/****** Object:  StoredProcedure [dbo].[DCISCheckSDID andTempID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISCheckSDID andTempID](@TemplateId varchar(20),@SupportingDocumentId varchar(20))
AS
BEGIN
select
b.TemplateName,
c.SupportingDocumentName,
b.TemplateId,
c.SupportingDocumentId

from tblTemplateSupportingDocument a,tblTemplateHeader b, tblSupportingDocuments c
where b.TemplateId=a.TemplateId  and c.SupportingDocumentId=a.SupportingDocumentId and   a.TemplateId like @TemplateId and a.SupportingDocumentId like @SupportingDocumentId
and a.IsActive='y'

END








GO
/****** Object:  StoredProcedure [dbo].[DCIScheckTaxCode]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCIScheckTaxCode] 
	(@taxCode varchar(20),@Customer varchar(20))
AS
BEGIN
	SELECT * FROM tblCustomerApplicableTax WHERE CustomerId=@Customer AND TaxCode=@taxCode
END



GO
/****** Object:  StoredProcedure [dbo].[DCISCheckUserGroup]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISCheckUserGroup] 
	(@CustomerId varchar(20),@UserGroupId varchar(20),@isActive varchar(1))
AS
BEGIN
	SELECT *
	FROM
	tblUser
	WHERE
	CustomerId=@CustomerId
	AND
	IsActive=@isActive
	AND
	UserGroupID=@UserGroupId
END





GO
/****** Object:  StoredProcedure [dbo].[DCIScheckUserName]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCIScheckUserName] 
	(@userId varchar(20))
AS
BEGIN
	
	SELECT * FROM tblUser WHERE UserID=@userId
END





GO
/****** Object:  StoredProcedure [dbo].[DCIScheckVATCustomer]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCIScheckVATCustomer]
	(@CustomerId varchar(20),@VATID varchar(20))
AS
BEGIN
	SELECT TaxCode FROM tblCustomerApplicableTax
	WHERE 
	CustomerId=@CustomerId
	AND
	TaxCode=@VATID

END




GO
/****** Object:  StoredProcedure [dbo].[DCISCreateUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISCreateUser]
(@UserId varchar(20), 
@UserGroupID nvarchar(20),
@PersonName nvarchar(150), 
@Password nvarchar(200), 
@CreatedBy nvarchar(50),
@IsActive varchar(1),
@IsVat varchar(5)
)
AS
BEGIN
	
	insert into tblUser
	(UserId, PersonName, UserGroupID, Password, CreatedBy, CreatedDate, UpdateDate,IsActive,PassowordExpiryDate)
	values
	(@UserId, @PersonName, @UserGroupID, @Password, @CreatedBy, getdate(),getdate(),@IsActive,GETDATE())

END










GO
/****** Object:  StoredProcedure [dbo].[DCISDeactivateUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISDeactivateUser]
(@UserId varchar(20),
 @Isactive varchar(1)
							)
AS
BEGIN

update tblUser
set isactive=@Isactive,
	UpdateDate=getDate()
where UserId=@UserId

END













GO
/****** Object:  StoredProcedure [dbo].[DCISDeactivatSignatureLevel]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISDeactivatSignatureLevel]
(@UserId varchar(20),
 @Isactive varchar(1),@TemplateId varchar(20))
							
AS
BEGIN

update tblSignatureLevels
set IsActive=@Isactive,
	ModifiedDate=getDate()
where UserId=@UserId and TemplateId=@TemplateId

END












GO
/****** Object:  StoredProcedure [dbo].[DCISDeleteCertificateDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DCISDeleteCertificateDetail]
@SeqNo  varchar(150)

AS

DELETE FROM [dbo].[tblCertificateRequestDetails]
WHERE SeqNo = @SeqNo










GO
/****** Object:  StoredProcedure [dbo].[DCISdeleteCustomerReffrenect]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create proc [dbo].[DCISdeleteCustomerReffrenect] (@RequestId varchar(20))
as begin
Delete from tblCustomerRequestReffrence where RequestId = @RequestId
End

GO
/****** Object:  StoredProcedure [dbo].[DCISdeleteEmail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISdeleteEmail]
	(@emailId int)
AS
BEGIN
	DELETE FROM tblWaitingEmail Where EmailId=@emailId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISDeleteUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISDeleteUser]
@UserID  varchar(150)

AS

DELETE FROM [dbo].[tblUser]
WHERE UserID = @UserID 










GO
/****** Object:  StoredProcedure [dbo].[DCISgetAdminRejectReason]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetAdminRejectReason] 
	(@RejectCategory varchar(20))
AS
BEGIN
	
	SELECT ReasonName,RejectCode
	FROM
	tblRejectReasons
	WHERE
	Category=@RejectCategory

END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllCertificateCanceldetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetAllCertificateCanceldetails] 
	(@CustomerId varchar(20), @Status varchar(1), @startdate varchar(8), @enddate varchar(8),@invoiceSupDoc varchar(20),@refNo varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if(@refNo='All')
	SELECT
	A.CertificateId,C.CustomerName,A.CreatedDate,'CO' As docTypes,'NOT' As Invoice
	FROM 
	tblCertificate A,tblCertifcateRequestHeader B,tblCustomer C
	WHERE
	A.RequestId=B.RequestId
	AND
	C.CustomerId like @CustomerId
	AND
	A.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )
	AND
	B.RequestId NOT in
	(SELECT RequestNo FROM tblInvoiceDetail)

	AND
	B.CustomerId=C.CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate
	UNION
	SELECT
	A.CertificateId,C.CustomerName,A.CreatedDate,'CO' As docTypes,'NOT' As Invoice
	FROM 
	tblCertificate A,tblUploadBasedCertificateRequest B,tblCustomer C
	WHERE
	A.RequestId=B.RequestId
	AND
	B.RequestId NOT in
	(SELECT RequestNo FROM tblInvoiceDetail)
	AND
	
	A.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)
	AND
	B.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate
	UNION
	SELECT
	A.RequestID,C.CustomerName,A.ApprovedDate AS CreatedDate,'Other Document' As docTypes,'NOT' As Invoice
	FROM 
	tblSupportingDocApproveRequest A,tblCustomer C
	WHERE
	A.Status=@Status
	AND
	A.SupportingDocID!=@invoiceSupDoc
	AND
	A.RequestId NOT in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.ApprovedDate,112) >=@startdate
	and convert(varchar,A.ApprovedDate,112) <=@enddate

		UNION
	SELECT
	A.RequestID,C.CustomerName,A.ApprovedDate AS CreatedDate,'Invoice' As docTypes,'NOT' As Invoice
	FROM 
	tblSupportingDocApproveRequest A,tblCustomer C
	WHERE
	A.Status=@Status
	AND
	A.SupportingDocID=@invoiceSupDoc
	AND
	A.RequestId NOT in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.ApprovedDate,112) >=@startdate
	and convert(varchar,A.ApprovedDate,112) <=@enddate

	UNION
	SELECT
	A.ReferenceNo As RequestID,C.CustomerName, A.CreatedDate AS CreatedDate,'CO' As docTypes,'NOT' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'C'
	AND
	A.ReferenceNo NOT in
	(SELECT ReferenceNo FROM tblInvoiceDetail)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate


	UNION
	SELECT
	A.ReferenceNo As RequestID,C.CustomerName, A.CreatedDate AS CreatedDate,'Invoice' As docTypes,'NOT' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'I'
	AND
	A.ReferenceNo NOT in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate


	UNION
	SELECT
	A.ReferenceNo As RequestID,C.CustomerName, A.CreatedDate AS CreatedDate,'Other Document' As docTypes,'NOT' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'O'
	AND
	A.ReferenceNo NOT in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate


	UNION

		SELECT
	A.CertificateId,C.CustomerName,A.CreatedDate,'CO' As docTypes,'In' As Invoice
	FROM 
	tblCertificate A,tblCertifcateRequestHeader B,tblCustomer C
	WHERE
	A.RequestId=B.RequestId
	AND
	C.CustomerId like @CustomerId
	AND
	A.CertificateId NOT In
	(SELECT DocumentId FROM tblCancelledcertificate)
	AND
	B.RequestId  in
	(SELECT D.RequestNo FROM tblInvoiceDetail D)

	AND
	B.CustomerId=C.CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate
	UNION
	SELECT
	A.CertificateId,C.CustomerName,A.CreatedDate,'CO' As docTypes,'In' As Invoice
	FROM 
	tblCertificate A,tblUploadBasedCertificateRequest B,tblCustomer C
	WHERE
	A.RequestId=B.RequestId
	AND
	B.RequestId  in
	(SELECT D.RequestNo FROM tblInvoiceDetail D)
	AND
	
	A.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D)
	AND
	B.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate
	UNION
	SELECT
	A.RequestID,C.CustomerName,A.ApprovedDate AS CreatedDate,'Other Document' As docTypes,'In' As Invoice
	FROM 
	tblSupportingDocApproveRequest A,tblCustomer C
	WHERE
	A.Status=@Status
	AND
	A.SupportingDocID!=@invoiceSupDoc
	AND
	A.RequestId  in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.ApprovedDate,112) >=@startdate
	and convert(varchar,A.ApprovedDate,112) <=@enddate

		UNION
	SELECT
	A.RequestID,C.CustomerName,A.ApprovedDate AS CreatedDate,'Invoice' As docTypes,'In' As Invoice
	FROM 
	tblSupportingDocApproveRequest A,tblCustomer C
	WHERE
	A.Status=@Status
	AND
	A.SupportingDocID=@invoiceSupDoc
	AND
	A.RequestId  in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.ApprovedDate,112) >=@startdate
	and convert(varchar,A.ApprovedDate,112) <=@enddate

	
	--new added(2016-11-08)---
	UNION
	SELECT
	A.ReferenceNo AS RequestID,C.CustomerName,A.CreatedDate AS CreatedDate,'CO' As docTypes,'In' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'C'
	AND
	A.ReferenceNo  in
	(SELECT ReferenceNo FROM tblInvoiceDetail)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate


	UNION
	SELECT
	A.ReferenceNo AS RequestID,C.CustomerName,A.CreatedDate AS CreatedDate,'Invoice' As docTypes,'In' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'I'
	AND
	A.ReferenceNo  in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate

	UNION
	SELECT
	A.ReferenceNo AS RequestID,C.CustomerName,A.CreatedDate AS CreatedDate,'Other Document' As docTypes,'In' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'O'
	AND
	A.ReferenceNo  in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	C.CustomerId like @CustomerId
	AND convert(varchar,A.CreatedDate,112) >=@startdate
	and convert(varchar,A.CreatedDate,112) <=@enddate
	Order By CreatedDate

	else
	SELECT
	A.CertificateId,C.CustomerName,A.CreatedDate,'CO' As docTypes,'NOT' As Invoice
	FROM 
	tblCertificate A,tblCertifcateRequestHeader B,tblCustomer C
	WHERE
	A.RequestId=B.RequestId
	AND
	A.CertificateId=@refNo
	AND
	A.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )
	AND
	B.RequestId NOT in
	(SELECT RequestNo FROM tblInvoiceDetail)

	AND
	B.CustomerId=C.CustomerId
	
	UNION
	SELECT
	A.CertificateId,C.CustomerName,A.CreatedDate,'CO' As docTypes,'NOT' As Invoice
	FROM 
	tblCertificate A,tblUploadBasedCertificateRequest B,tblCustomer C
	WHERE
	A.RequestId=B.RequestId
	AND
	B.RequestId NOT in
	(SELECT RequestNo FROM tblInvoiceDetail)
	AND
	
	A.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)
	AND
	B.CustomerId=C.CustomerId
	AND
	A.CertificateId=@refNo
	UNION
	SELECT
	A.RequestID,C.CustomerName,A.ApprovedDate AS CreatedDate,'Other Document' As docTypes,'NOT' As Invoice
	FROM 
	tblSupportingDocApproveRequest A,tblCustomer C
	WHERE
	A.Status=@Status
	AND
	A.SupportingDocID!=@invoiceSupDoc
	AND
	A.RequestId NOT in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.RequestID=@refNo

		UNION
	SELECT
	A.RequestID,C.CustomerName,A.ApprovedDate AS CreatedDate,'Invoice' As docTypes,'NOT' As Invoice
	FROM 
	tblSupportingDocApproveRequest A,tblCustomer C
	WHERE
	A.Status=@Status
	AND
	A.SupportingDocID=@invoiceSupDoc
	AND
	A.RequestId NOT in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.RequestID=@refNo

	UNION
	SELECT
	A.ReferenceNo As RequestID,C.CustomerName, A.CreatedDate AS CreatedDate,'CO' As docTypes,'NOT' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'C'
	AND
	A.ReferenceNo NOT in
	(SELECT ReferenceNo FROM tblInvoiceDetail)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.ReferenceNo=@refNo


	UNION
	SELECT
	A.ReferenceNo As RequestID,C.CustomerName, A.CreatedDate AS CreatedDate,'Invoice' As docTypes,'NOT' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'I'
	AND
	A.ReferenceNo NOT in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.ReferenceNo=@refNo


	UNION
	SELECT
	A.ReferenceNo As RequestID,C.CustomerName, A.CreatedDate AS CreatedDate,'Other Document' As docTypes,'NOT' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'O'
	AND
	A.ReferenceNo NOT in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.ReferenceNo=@refNo


	UNION

		SELECT
	A.CertificateId,C.CustomerName,A.CreatedDate,'CO' As docTypes,'In' As Invoice
	FROM 
	tblCertificate A,tblCertifcateRequestHeader B,tblCustomer C
	WHERE
	A.RequestId=B.RequestId
	AND
	A.CertificateId=@refNo
	AND
	A.CertificateId NOT In
	(SELECT DocumentId FROM tblCancelledcertificate)
	AND
	B.RequestId  in
	(SELECT D.RequestNo FROM tblInvoiceDetail D)

	AND
	B.CustomerId=C.CustomerId
	
	UNION
	SELECT
	A.CertificateId,C.CustomerName,A.CreatedDate,'CO' As docTypes,'In' As Invoice
	FROM 
	tblCertificate A,tblUploadBasedCertificateRequest B,tblCustomer C
	WHERE
	A.RequestId=B.RequestId
	AND
	B.RequestId  in
	(SELECT D.RequestNo FROM tblInvoiceDetail D)
	AND
	
	A.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D)
	AND
	B.CustomerId=C.CustomerId
	AND
	A.CertificateId=@refNo
	UNION
	SELECT
	A.RequestID,C.CustomerName,A.ApprovedDate AS CreatedDate,'Other Document' As docTypes,'In' As Invoice
	FROM 
	tblSupportingDocApproveRequest A,tblCustomer C
	WHERE
	A.Status=@Status
	AND
	A.SupportingDocID!=@invoiceSupDoc
	AND
	A.RequestId  in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.RequestID=@refNo

		UNION
	SELECT
	A.RequestID,C.CustomerName,A.ApprovedDate AS CreatedDate,'Invoice' As docTypes,'In' As Invoice
	FROM 
	tblSupportingDocApproveRequest A,tblCustomer C
	WHERE
	A.Status=@Status
	AND
	A.SupportingDocID=@invoiceSupDoc
	AND
	A.RequestId  in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.RequestID=@refNo

	
	--new added(2016-11-08)---
	UNION
	SELECT
	A.ReferenceNo AS RequestID,C.CustomerName,A.CreatedDate AS CreatedDate,'CO' As docTypes,'In' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'C'
	AND
	A.ReferenceNo  in
	(SELECT ReferenceNo FROM tblInvoiceDetail)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.ReferenceNo=@refNo


	UNION
	SELECT
	A.ReferenceNo AS RequestID,C.CustomerName,A.CreatedDate AS CreatedDate,'Invoice' As docTypes,'In' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'I'
	AND
	A.ReferenceNo  in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.ReferenceNo=@refNo

	UNION
	SELECT
	A.ReferenceNo AS RequestID,C.CustomerName,A.CreatedDate AS CreatedDate,'Other Document' As docTypes,'In' As Invoice
	FROM 
	tblManualCertificate A,tblCustomer C
	WHERE
	A.Status like 'Y'
	AND
	A.ItemDescription like 'O'
	AND
	A.ReferenceNo  in
	(SELECT SupportingDocName FROM tblInvoiceRate)
	AND
	A.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate)

	AND
	A.CustomerId=C.CustomerId
	AND
	A.ReferenceNo=@refNo
	Order By CreatedDate
	
END

GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllCertificateheaderM]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetAllCertificateheaderM]
(@RequestId varchar(20))
AS
BEGIN

	SELECT * FROM tblCertifcateRequestHeader

	where RequestId=@RequestId
END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllCustomer]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetAllCustomer]

AS
BEGIN

	SELECT CustomerId,CustomerName FROM tblCustomer order By CustomerName ASC
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllCustomerDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetAllCustomerDetail]
	(@CustomerId varchar(20),@GroupId varchar(20))
AS
BEGIN
	SELECT
	A.CustomerName,A.CustomerId,A.CreatedDate,A.Address1,A.Address2,A.Address3,
	A.ContactPersonDesignation,A.ContactPersonDirectPhoneNumber,A.ContactPersonEmail,
	A.ContactPersonMobile,A.ContactPersonName,A.CreatedBy,A.Email,A.Fax,
	A.NCEMember,A.ProductDetails,A.Telephone,B.TemplateId,C.TemplateName,A.PaidType,A.IsSVat,
	D.UserID
	FROM
	tblCustomer A,tblCustomerTemplate B,
	tblTemplateHeader C,tblUser D
	WHERE
	A.CustomerId Like @CustomerId
	AND
	A.CustomerId=D.CustomerId
	AND
	D.UserGroupID like @GroupId
	AND
	D.IsActive like'Y'
	AND
	B.CustomerId=A.CustomerId
	AND
	B.TemplateId=C.TemplateId
	

	Order By A.CustomerName
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllExportSector]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetAllExportSector] 
	(@Status varchar(1))
AS
BEGIN
	SELECT 
	ExportSector,ExportId
	FROM
	tblExportSector
	WHERE
	Status=@Status
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllInvoice]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetAllInvoice] 
	(@startdate varchar(8),@enddate varchar(8),@Status varchar(1),@CustomerId varchar(20))
AS
BEGIN
	
	select
a.CustomerId,
a.CustomerName
from tblCustomer a,tblCertifcateRequestHeader b,tblCertificate c
where a.CustomerId = b.CustomerId
and
b.Status=@Status
AND a.CustomerId like @CustomerId
and

	C.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=C.CertificateId)
and
b.RequestId
not in
(
	SELECT e.RequestNo FROM tblInvoiceDetail e WHERE e.RequestNo=b.RequestId
	---and convert(varchar,e.CreatedDate,112) >=@startdate
	--and convert(varchar,e.CreatedDate,112) <=@enddate
	
	
)
and b.RequestId= c.RequestId
and convert(varchar,b.CreatedDate,112) >=@startdate
and convert(varchar,b.CreatedDate,112) <=@enddate

Group By a.CustomerId, a.CustomerName
UNION
select
a.CustomerId,
a.CustomerName
from tblCustomer a,tblSupportingDocApproveRequest b
where a.CustomerId = b.CustomerId
and
b.Status=@Status
AND a.CustomerId like @CustomerId
and

	b.RequestID NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=b.RequestID)
and
b.RequestId
not in
(
	SELECT e.SupportingDocName FROM tblInvoiceRate e WHERE e.SupportingDocName=b.RequestId
	---and convert(varchar,e.CreatedDate,112) >=@startdate
	--and convert(varchar,e.CreatedDate,112) <=@enddate
	
	
)
and convert(varchar,b.ApprovedDate,112) >=@startdate
and convert(varchar,b.ApprovedDate,112) <=@enddate

Group By a.CustomerId, a.CustomerName
UNION
	select
a.CustomerId,
a.CustomerName
from tblCustomer a,tblUploadBasedCertificateRequest b,tblCertificate C
where a.CustomerId = b.CustomerId
and
b.Status=@Status
AND a.CustomerId like @CustomerId
and
C.RequestId=b.RequestId
AND
	C.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=C.CertificateId)
and
b.RequestId
not in
(
	SELECT e.RequestNo FROM tblInvoiceDetail e WHERE e.RequestNo=b.RequestId
	---and convert(varchar,e.CreatedDate,112) >=@startdate
	--and convert(varchar,e.CreatedDate,112) <=@enddate
	
	
)
and convert(varchar,b.CreatedDate,112) >=@startdate
and convert(varchar,b.CreatedDate,112) <=@enddate

Group By a.CustomerId, a.CustomerName



UNION
	select
a.CustomerId,
a.CustomerName
from tblCustomer a,tblManualCertificate b
where a.CustomerId = b.CustomerId
and
b.Status like 'Y'--@Status
AND 
a.CustomerId like @CustomerId

AND
	b.ReferenceNo NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=b.ReferenceNo)
and
b.ReferenceNo
not in
(
	SELECT e.RequestNo FROM tblInvoiceDetail e WHERE e.RequestNo=b.ReferenceNo
	---and convert(varchar,e.CreatedDate,112) >=@startdate
	--and convert(varchar,e.CreatedDate,112) <=@enddate
	
	
)
and convert(varchar,b.CreatedDate,112) >=@startdate
and convert(varchar,b.CreatedDate,112) <=@enddate

Group By a.CustomerId, a.CustomerName
END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllMassage]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[DCISgetAllMassage]
	(@StartDate varchar(8),@EndDate varchar(8),@status varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * FROM tblContactFormDetails 
	WHERE
	 convert(varchar,CreatedDate,112) >=@StartDate
	and 
	convert(varchar,CreatedDate,112) <=@EndDate
	and
	ViewStatus like @status
	order By CreatedDate ASC
END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllNewMassage]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[DCISgetAllNewMassage]
	(@status varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT * FROM tblContactFormDetails 
	WHERE
	ViewStatus like @status
	order By CreatedDate ASC
END



GO
/****** Object:  StoredProcedure [dbo].[DCISGETALLPENDINGCERTIFICATE]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE
 PROC [dbo].[DCISGETALLPENDINGCERTIFICATE](@CustomerID varchar(20))
AS BEGIN
Select 
'W' as CertificateType,
'Web Based' as CType,
W.RequestId, 
CertificatePath, 
CertificateName,
C.CustomerName, 
C.CustomerId,
C.ContactPersonEmail,
H.RequestDate, 
H.InvoiceNo, 
W.Status,
H.TemplateId,
H.SealRequired as SealRequired,
'YES' as CollectionType,
 CONCAT(U.PersonName, '-' ,U.Designation) as Createdby,
 Q.SummaryDesc

From tblWebBasedCertificateRequest W , tblCertifcateRequestHeader H , tblCustomer C ,tblCountry D, tblUser U , tblCertificateRequestDetails Q
Where W.RequestId = H.RequestId
And H.CustomerId = C.CustomerId
and q.RequestId = h.RequestId
And D.CountryCode = H.CountryCode
And H.Status = 'G'
And W.Status = 'P'
And U.UserID = H.CreatedBy
And H.CustomerId like @CustomerID
And H.SealRequired like 'True'


union

Select 
'W' as CertificateType,
'Web Based' as CType,
W.RequestId, 
CertificatePath, 
CertificateName,
C.CustomerName, 
C.CustomerId,
C.ContactPersonEmail,
H.RequestDate, 
H.InvoiceNo, 
W.Status,
H.TemplateId,
H.SealRequired as SealRequired,
'NO' as CollectionType,
 CONCAT(U.PersonName, '-' ,U.Designation) as Createdby,
 Q.SummaryDesc

From tblWebBasedCertificateRequest W , tblCertifcateRequestHeader H , tblCustomer C ,tblCountry D,tblUser U,tblCertificateRequestDetails Q
Where W.RequestId = H.RequestId
And H.CustomerId = C.CustomerId
and q.RequestId = h.RequestId
And D.CountryCode = H.CountryCode
And H.Status = 'G'
And W.Status = 'P'
And U.UserID = H.CreatedBy
And H.CustomerId like @CustomerID
And H.SealRequired like 'False'

union

--eeeeeeeeee
Select 
'W' as CertificateType,
'Web Based' as CType,
W.RequestId, 
CertificatePath, 
CertificateName,
C.CustomerName, 
C.CustomerId,
C.ContactPersonEmail,
H.RequestDate, 
H.InvoiceNo, 
W.Status,
H.TemplateId,
H.SealRequired as SealRequired,
'YES' as CollectionType,
 CONCAT(U.PersonName, '-' ,U.Designation) as Createdby,
 Q.GoodDetails as SummaryDesc

From tblWebBasedCertificateRequest W , tblCertifcateRequestHeader H , tblCustomer C ,tblCountry D, tblUser U , tblRowCertificateRequestDetails Q
Where W.RequestId = H.RequestId
And H.CustomerId = C.CustomerId
and q.RequestId = h.RequestId
And D.CountryCode = H.CountryCode
And H.Status = 'G'
And W.Status = 'P'
And U.UserID = H.CreatedBy
And H.CustomerId like @CustomerID
And H.SealRequired like 'True'


union

Select 
'W' as CertificateType,
'Web Based' as CType,
W.RequestId, 
CertificatePath, 
CertificateName,
C.CustomerName, 
C.CustomerId,
C.ContactPersonEmail,
H.RequestDate, 
H.InvoiceNo, 
W.Status,
H.TemplateId,
H.SealRequired as SealRequired,
'NO' as CollectionType,
 CONCAT(U.PersonName, '-' ,U.Designation) as Createdby,
 Q.GoodDetails as SummaryDesc

From tblWebBasedCertificateRequest W , tblCertifcateRequestHeader H , tblCustomer C ,tblCountry D,tblUser U,tblRowCertificateRequestDetails Q
Where W.RequestId = H.RequestId
And H.CustomerId = C.CustomerId
and q.RequestId = h.RequestId
And D.CountryCode = H.CountryCode
And H.Status = 'G'
And W.Status = 'P'
And U.UserID = H.CreatedBy
And H.CustomerId like @CustomerID
And H.SealRequired like 'False'

union
--eeeeeeeeee

Select 
'U' as CertificateType,
'Upload Based' as CType,
RequestId, 
UploadPath as CertificatePath,
RequestId+'_Upload_Cert.pdf'  as CertificateName,
C.CustomerName, 
C.CustomerId,
C.ContactPersonEmail,
RequestDate, 
InvoiceNo, 
U.Status,
null as TemplateId,
U.SealRequired as SealRequired,
'YES' as CollectionType,
 CONCAT(D.PersonName, '-' ,D.Designation) as Createdby,
 'Not Available' as SummaryDesc
From tblUploadBasedCertificateRequest U, tblCustomer C,tblUser D
where U.CustomerId = c.CustomerId
and U.Status = 'P'
And D.UserID = U.CreatedBy
and U.CustomerId like @CustomerID
and U.SealRequired like 'True'

union

Select 
'U' as CertificateType,
'Upload Based' as CType,
RequestId, 
UploadPath as CertificatePath,
RequestId+'_Upload_Cert.pdf'  as CertificateName,
C.CustomerName, 
C.CustomerId,
C.ContactPersonEmail,
RequestDate, 
InvoiceNo, 
U.Status,
null as TemplateId,
U.SealRequired as SealRequired,
'NO' as CollectionType,
 CONCAT(D.PersonName, '-' ,D.Designation) as Createdby,
  'Not Available' as SummaryDesc
From tblUploadBasedCertificateRequest U, tblCustomer C,tblUser D
where U.CustomerId = c.CustomerId
and U.Status = 'P'
And D.UserID = U.CreatedBy
and U.CustomerId like @CustomerID
and U.SealRequired like 'False'



order by RequestDate

END

GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllRateDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetAllRateDetails]
	(@status varchar(2))
AS
BEGIN
	SELECT
	*
	FROM
	tblRates A
	WHERE
	Status=@status

END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllTemplateDownload]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISgetAllTemplateDownload]
AS BEGIN
SELECT IndexNo, TemplateID, TemplateDName, TemplateIMGPath, DownloadPath, DownlaodedTime
from tblTemplateDownload
END


GO
/****** Object:  StoredProcedure [dbo].[DCISgetAllUserUsingCustomerId]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetAllUserUsingCustomerId]
	(@CustomerId varchar(20),@IsActive varchar(1))

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT
	A.CreatedBy,A.CreatedDate,A.PersonName,A.UserID,B.GroupName,A.UserGroupID
	FROM
	tblUser A,tblUserGroup B
	WHERE
	A.CustomerId=@CustomerId
	AND
	A.IsActive=@IsActive
	AND
	A.UserGroupID=B.GroupId

END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetattachdocumentdetailsfordown]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISgetattachdocumentdetailsfordown](@Certid  varchar(20))
AS
BEGIN
	Select a.RequestRefNo,a.UploadedPath,b.SupportingDocumentName,a.SupportingDocumentId from tblSupportingDOCUpload  a ,tblSupportingDocuments b  where RequestRefNo like @Certid 
	and a.SupportingDocumentId=b.SupportingDocumentId
	and a.Remarks like ''
END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetCanceleCertificate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCanceleCertificate]
	(@startDate varchar(8),@enddate varchar(8),@CustomerId varchar(20),@refNo varchar(20))
AS
BEGIN
if(@refNo='All')
	SELECT A.DocumentId,B.CustomerName,A.Remark,A.CancelBy,A.CancelDate,A.DocumentType FROM
	 tblCancelledcertificate A,tblCustomer B
	 WHERE
	 A.CustomerId=B.CustomerId
	
	 AND
	 A.CustomerId like @CustomerId
	
	and convert(varchar,A.CancelDate,112) >=@startdate

	and convert(varchar,A.CancelDate,112) <=@enddate
else

SELECT A.DocumentId,B.CustomerName,A.Remark,A.CancelBy,A.CancelDate,A.DocumentType FROM
	 tblCancelledcertificate A,tblCustomer B
	 WHERE
	 A.CustomerId=B.CustomerId
	 AND
	 A.DocumentId=@refNo



END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateConsigneeFindDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCertificateConsigneeFindDetails]
	(@CustomerId varchar(20),@status varchar(1),@fromdate date,@todate date,@COno varchar(20))
AS
BEGIN
	SELECT 
	A.Consignee,A.Consignor,A.InvoiceNo,C.CreatedDate,B.CustomerName,C.CertificateId,C.CertificatePath,A.RequestDate
	FROM
	tblCertifcateRequestHeader A,tblCustomer B,tblCertificate C
	WHERE
	C.CertificateId like @COno
	And
	A.CustomerId like @CustomerId
	AND
	A.CustomerId=B.CustomerId
	AND
	A.Status=@status
	AND
	A.RequestId=C.RequestId
	AND
	C.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=C.CertificateId)
	AND
	convert(varchar,C.CreatedDate,112) >=@fromdate
	AND 
	convert(varchar,C.CreatedDate,112) <=@Todate
	UNION
	SELECT 
	null AS Consignee,B.CustomerName AS Consignor,A.InvoiceNo,A.CreatedDate,B.CustomerName,A.CertificateId,C.CertificatePath,A.RequestDate
	FROM
	tblUploadBasedCertificateRequest A,tblCustomer B,tblCertificate C
	WHERE
	C.CertificateId like @COno
	AND
	A.CustomerId like @CustomerId
	AND
	A.CustomerId=B.CustomerId
	AND
	A.Status=@status
	AND
	C.RequestId=A.RequestId
	AND
	C.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=C.CertificateId)

	AND
	convert(varchar,A.CreatedDate,112) >=@fromdate
	AND 
	convert(varchar,A.CreatedDate,112) <=@Todate
	UNION
	SELECT 
	null AS Consignee,B.CustomerName AS Consignor,A.ExporterInvoiceNo As InvoiceNo,A.CreatedDate,B.CustomerName,A.ReferenceNo As CertificateId,null As CertificatePath,null As RequestDate
	FROM
	tblManualCertificate A,tblCustomer B
	WHERE
	A.ReferenceNo like @COno
	AND
	A.CustomerId like @CustomerId
	AND
	A.CustomerId=B.CustomerId
	AND
	A.Status like 'Y'
	AND
	A.ItemDescription like 'C'
	AND
	A.ReferenceNo NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.ReferenceNo)

	AND
	convert(varchar,A.CreatedDate,112) >=@fromdate
	AND 
	convert(varchar,A.CreatedDate,112) <=@Todate
	
	order by CreatedDate DESC
END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCertificateDetails]
	(@CertificateNo varchar(20))
AS
BEGIN
	SELECT A.CertificateId,A.CreatedDate,A.ExpiryDate,A.IsDownloaded,A.CreatedBy,A.CertificateName,A.RequestId,C.CustomerName,A.CertificatePath,C.CustomerId
	FROM tblCertificate A,tblCertifcateRequestHeader B,tblCustomer C
	WHERE A.CertificateId=@CertificateNo
	AND
	A.RequestId=B.RequestId
	AND
	B.CustomerId=C.CustomerId
	UNION
	SELECT A.CertificateId,A.CreatedDate,A.ExpiryDate,A.IsDownloaded,A.CreatedBy,A.CertificateName,A.RequestId,C.CustomerName ,A.CertificatePath,C.CustomerId
	FROM tblCertificate A,tblUploadBasedCertificateRequest B,tblCustomer C
	WHERE A.CertificateId=@CertificateNo
	AND
	A.RequestId=B.RequestId
	AND
	B.CustomerId=C.CustomerId

	UNION
	SELECT A.SupportingDocID As CertificateId,A.ApprovedDate As CreatedDate,A.ApprovedDate AS ExpiryDate,A.IsDownloaded,A.ApprovedBy As CreatedBy,A.UploadDocName,A.RequestId,C.CustomerName ,A.UploadPath AS CertificatePath,C.CustomerId
	FROM tblSupportingDocApproveRequest A,tblCustomer C
	WHERE A.RequestID=@CertificateNo
	AND
	A.CustomerId=C.CustomerId

END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateDetailsM]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetCertificateDetailsM](@ReqID  varchar(10))
AS
BEGIN
	Select SeqNo,GoodItem,ShippingMark,PackageType,SummaryDesc,Quantity,HSCode from tblCertificateRequestDetails where RequestId like @ReqID
END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateIssueDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCertificateIssueDetails]
	(@InvoiceNo varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
	 UnitCharge,COUNT(RequestNo) AS certificaterow 
	 From 
	 tblInvoiceDetail 
	 WHERE
	 InvoiceNo=@InvoiceNo
GROUP By UnitCharge

    
END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateRejectResons]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetCertificateRejectResons]
as
Begin
Select RejectCode, ReasonName, Category, IsActive
from tblRejectReasons
where Category like 'CERTIFICATE'
and IsActive like 'Y'

End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateRequestByID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[DCISgetCertificateRequestByID](@RequestNo varchar(20))
as begin


select * from tblCertifcateRequestHeader
where RequestId = @RequestNo


 End



 





GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateRequests]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetCertificateRequests](@CustomerId varchar(20),@Status varchar(2))
as begin


select 

RequestId, 
TemplateId,
A.CustomerId,
C.CustomerName, 
RequestDate, 
ModifiedDate,   
c.Status,
 Consignor, 
 Consignee, 
 InvoiceNo, 
 InvoiceDate, 
 B.CountryName, 
 LoadingPort, 
 PortOfDischarge, 
 Vessel, 
 PlaceOfDelivery, 
 TotalInvoiceValue, 
 TotalQuantity

 from tblCertifcateRequestHeader A,tblCountry B,tblCustomer C
 where A.CustomerId = c.CustomerId
 and A.CountryCode = B.CountryCode
 and A.CustomerId like @CustomerId
 and A.Status like @Status
 order by RequestDate

 End



 





GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateRequestsByUserid]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetCertificateRequestsByUserid](@UserId varchar(20),@Status varchar(2))
as begin


select 

RequestId, 
TemplateId,
A.CustomerId, 
RequestDate, 
ModifiedDate,   
A.Status,
 Consignor, 
 Consignee, 
 InvoiceNo, 
 InvoiceDate, 
 B.CountryName, 
 LoadingPort, 
 PortOfDischarge, 
 Vessel, 
 PlaceOfDelivery, 
 TotalInvoiceValue, 
 TotalQuantity

 from tblCertifcateRequestHeader A,tblCountry B,tblCustomer C,tblUser D
 where A.CustomerId = c.CustomerId
 and A.CountryCode = B.CountryCode
 and A.CustomerId =D.CustomerId
and D.CustomerId=@UserId
 and A.Status = @Status


 End






GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISgetCertificateStatus] 
	(@CertificateNo varchar(20))
AS
BEGIN
	SELECT
	B.CustomerName,A.CreatedDate as InvoiceDate,A.Consignee,A.InvoiceNo,A.TotalInvoiceValue
	FROM
	tblCertifcateRequestHeader A,tblCustomer B,tblCertificate C
	WHERE
	A.CustomerId=B.CustomerId
	AND
	A.RequestId=c.RequestId
	AND
	A.Status='A'
	AND
	C.CertificateId=@CertificateNo
	AND
	C.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=C.CertificateId)

	UNION
	SELECT
	B.CustomerName,A.CreatedDate as InvoiceDate,Null AS Consignee,A.InvoiceNo,NULL AS TotalInvoiceValue
	FROM
	tblUploadBasedCertificateRequest A,tblCustomer B
	WHERE
	A.CustomerId=B.CustomerId
	AND
	A.CertificateId=@CertificateNo
	AND
	
	A.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.CertificateId)
	AND
	A.Status='A'

	UNION
	SELECT
	B.CustomerName,A.CreatedDate as InvoiceDate,Null AS Consignee,A.ExporterInvoiceNo As InvoiceNo,NULL AS TotalInvoiceValue
	FROM
	tblManualCertificate A,tblCustomer B
	WHERE
	A.CustomerId=B.CustomerId
	AND
	A.ReferenceNo=@CertificateNo
	AND
	
	A.ReferenceNo NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.ReferenceNo)
	AND
	A.Status like 'Y'
	
END


GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateStatusA]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetCertificateStatusA](@RequestId varchar(20),@Status varchar(2))
AS
BEGIN
	Select RequestId,CertificateName,CertificatePath,IsValid,IsDownloaded,ExpiryDate from tblCertificate where RequestId like @RequestId and IsDownloaded like @Status
END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertificateSuportingDocument]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[DCISgetCertificateSuportingDocument] 
	-- Add the parameters for the stored procedure here
	@status varchar(1),@CertificateRequestId varchar(20)
AS
BEGIN

	SELECT B.SupportingDocumentName FROM tblSupportingDocApproveRequest A,tblSupportingDocuments B WHERE CertificateRequestId=@CertificateRequestId AND Status =@status AND A.SupportingDocID=B.SupportingDocumentId
END


GO
/****** Object:  StoredProcedure [dbo].[DCISgetCertiReqstDetailByReqID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DCISgetCertiReqstDetailByReqID](@ReqID varchar(20))
as begin

select * from tblCertificateRequestDetails
where RequestId = @ReqID

End





GO
/****** Object:  StoredProcedure [dbo].[DCISgetConsignee]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetConsignee](@Status  varchar(2))
AS
BEGIN
	Select * from tblConsignee where Status like @Status 
END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetConsignor]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetConsignor](@Status  varchar(2))
AS
BEGIN
	Select * from tblConsignor where Status like @Status 
END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetContactformCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetContactformCount]
AS
BEGIN

select
	COUNT (seqNo)
	from
	tblContactFormDetails
	
	 where ViewStatus ='N' 


END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetCORegistry]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISgetCORegistry](@FromDate varchar(8),
@ToDate varchar(8),
@CertificateRateId varchar(10),
@InvoiceSupportingDocId varchar(10),@InvoicerateId varchar(10),
@OtherDocumentRateId varchar(10),
@CustomerId varchar(10),
@NCEMember varchar(8),
@Paytype varchar(10),
@SealRequired varchar(5)
)
    
    
AS
BEGIN
select Convert(varchar,A.CreatedDate,111) as CreatedDate,A.CertificateId,B.InvoiceNo,C.CustomerName,B.PortOfDischarge,
C.ExportSector,NCEMember,'CO' as ittemtype,1 as Qty,E.Rates, substring(Consignee, 0 , CHARINDEX('<', Consignee)-0)  as Consignee, substring(Consignor, 0 , CHARINDEX('<', Consignor)-0)  as Consignor
from tblCertificate A, tblCertifcateRequestHeader B, tblCustomer C,tblCustomerApplicableRates E
where A.RequestId=B.RequestId
and B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@CertificateRateId
and Convert(varchar,A.CreatedDate,112)>=@FromDate
and Convert(varchar,A.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype
and B.SealRequired like @SealRequired /** Digital Authenticaton Required Update ***/
and CertificateId not in 
(select DocumentId from tblCancelledCertificate)
union

select Convert(varchar,A.CreatedDate,111)as CreatedDate,A.CertificateId,B.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'CO' as ittemtype,1 as Qty,E.Rates,'' as Consignee,'' as Consignor
from tblCertificate A, tblUploadBasedCertificateRequest B, tblCustomer C,tblCustomerApplicableRates E
where A.RequestId=B.RequestId
and B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@CertificateRateId
and Convert(varchar,A.CreatedDate,112)>=@FromDate
and Convert(varchar,A.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype 
and B.SealRequired like @SealRequired /** Digital Authenticaton Required Update ***/
and A.CertificateId not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,D.CertificateId,H.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'Invoice' as ittemtype,1 as Qty,E.Rates, '' as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E, tblCertifcateRequestHeader H ,tblCertificate D
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@InvoicerateId
and A.CertificateRequestId = H.RequestId
and A.CertificateRequestId = D.RequestId
and H.RequestId = D.RequestId
and A.SupportingDocID =@InvoiceSupportingDocId
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,D.CertificateId,H.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'Invoice' as ittemtype,1 as Qty,E.Rates, '' as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E , tblUploadBasedCertificateRequest H, tblCertificate D
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@InvoicerateId
and a.CertificateRequestId = H.RequestId
and A.CertificateRequestId = D.RequestId
and H.RequestId = D.RequestId
and A.SupportingDocID =@InvoiceSupportingDocId
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,D.CertificateId,H.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,S.SupportingDocumentName as ittemtype,1 as Qty,E.Rates,''as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E,tblSupportingDocuments S , tblCertifcateRequestHeader H , tblCertificate D
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@OtherDocumentRateId
and A.CertificateRequestId = H.RequestId
and A.CertificateRequestId = D.RequestId
and H.RequestId = D.RequestId
and A.SupportingDocID !=@InvoiceSupportingDocId
and A.SupportingDocID !='SDID5'
and S.SupportingDocumentId = A.SupportingDocID
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember 
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,D.CertificateId,H.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,S.SupportingDocumentName as ittemtype,1 as Qty,E.Rates,''as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E,tblSupportingDocuments S, tblUploadBasedCertificateRequest H,tblCertificate D
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@OtherDocumentRateId
and A.CertificateRequestId = H.RequestId
and A.CertificateRequestId = D.RequestId
and H.RequestId = D.RequestId
and A.SupportingDocID !=@InvoiceSupportingDocId
and A.SupportingDocID !='SDID5' -- Attache CO Ignore Request by L A Pradeep
and S.SupportingDocumentId = A.SupportingDocID
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember 
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,a.RequestID as CertificateId,'S/D Req' as InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,S.SupportingDocumentName as ittemtype,1 as Qty,E.Rates,''as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E,tblSupportingDocuments S
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@InvoicerateId
and A.CertificateRequestId is null
and A.SupportingDocID =@InvoiceSupportingDocId
and S.SupportingDocumentId = A.SupportingDocID
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,a.RequestID as CertificateId,'S/D Req Other' as InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,S.SupportingDocumentName as ittemtype,1 as Qty,E.Rates,''as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E,tblSupportingDocuments S
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@OtherDocumentRateId
and A.CertificateRequestId is null
and A.SupportingDocID !=@InvoiceSupportingDocId
and A.SupportingDocID !='SDID5' -- Attache CO Ignore Request by L A Pradeep
and S.SupportingDocumentId = A.SupportingDocID
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember 
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union
select Convert(varchar,B.CreatedDate,111)as CreatedDate,ReferenceNo as CertificateId,ExporterInvoiceNo as InvoiceNo,
C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'CO' as ittemtype,1 as Qty,E.Rates,'' as Consignee,'' as Consignor
from tblManualCertificate B, tblCustomer C,tblCustomerApplicableRates E
where 
B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@CertificateRateId
and Convert(varchar,B.CreatedDate,112)>=@FromDate
and Convert(varchar,B.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype
and ItemDescription='C'
and ReferenceNo not in
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,B.CreatedDate,111)as CreatedDate,ReferenceNo as CertificateId,ExporterInvoiceNo as InvoiceNo,
C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'Invoice' as ittemtype,1 as Qty,E.Rates,'' as Consignee,'' as Consignor
from tblManualCertificate B, tblCustomer C,tblCustomerApplicableRates E
where 
B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@InvoicerateId
and Convert(varchar,B.CreatedDate,112)>=@FromDate
and Convert(varchar,B.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype 
and ItemDescription='I'
and ReferenceNo not in
(select DocumentId from tblCancelledCertificate)

union
select Convert(varchar,B.CreatedDate,111)as CreatedDate,ReferenceNo as CertificateId,ExporterInvoiceNo as InvoiceNo,
C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'Documents' as ittemtype,1 as Qty,E.Rates,'' as Consignee,'' as Consignor
from tblManualCertificate B, tblCustomer C,tblCustomerApplicableRates E
where 
B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@OtherDocumentRateId
and Convert(varchar,B.CreatedDate,112)>=@FromDate
and Convert(varchar,B.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember 
and C.PaidType like  @Paytype
and ItemDescription='O'
and ReferenceNo not in
(select DocumentId from tblCancelledCertificate)


END


GO
/****** Object:  StoredProcedure [dbo].[DCISgetCORegistryMnthSummry]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetCORegistryMnthSummry](@FromDate varchar(8),
@ToDate varchar(8),
@CertificateRateId varchar(10),
@InvoiceSupportingDocId varchar(10),@InvoicerateId varchar(10),
@OtherDocumentRateId varchar(10),
@CustomerId varchar(10),
@NCEMember varchar(8),
@Paytype varchar(10)
)
    
    
AS
BEGIN
SELECT SUM(Rates) AS TotalSales,SUM(Qty) as TotalQty, YEAR(CreatedDate) AS OrderYear, 
	MONTH(CreatedDate) AS OrderMonth, CustomerName from
(

select Convert(varchar,A.CreatedDate,111) as CreatedDate,A.CertificateId,B.InvoiceNo,C.CustomerName,B.PortOfDischarge,
C.ExportSector,NCEMember,'CO' as ittemtype,1 as Qty,E.Rates, substring(Consignee, 0 , CHARINDEX('<', Consignee)-0)  as Consignee, substring(Consignor, 0 , CHARINDEX('<', Consignor)-0)  as Consignor
from tblCertificate A, tblCertifcateRequestHeader B, tblCustomer C,tblCustomerApplicableRates E
where A.RequestId=B.RequestId
and B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@CertificateRateId
and Convert(varchar,A.CreatedDate,112)>=@FromDate
and Convert(varchar,A.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype
and CertificateId not in 
(select DocumentId from tblCancelledCertificate)
union

select Convert(varchar,A.CreatedDate,111)as CreatedDate,A.CertificateId,B.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'CO' as ittemtype,1 as Qty,E.Rates,'' as Consignee,'' as Consignor
from tblCertificate A, tblUploadBasedCertificateRequest B, tblCustomer C,tblCustomerApplicableRates E
where A.RequestId=B.RequestId
and B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@CertificateRateId
and Convert(varchar,A.CreatedDate,112)>=@FromDate
and Convert(varchar,A.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype 
and A.CertificateId not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,D.CertificateId,H.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'Invoice' as ittemtype,1 as Qty,E.Rates, '' as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E, tblCertifcateRequestHeader H ,tblCertificate D
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@InvoicerateId
and A.CertificateRequestId = H.RequestId
and A.CertificateRequestId = D.RequestId
and H.RequestId = D.RequestId
and A.SupportingDocID =@InvoiceSupportingDocId
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,D.CertificateId,H.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'Invoice' as ittemtype,1 as Qty,E.Rates, '' as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E , tblUploadBasedCertificateRequest H, tblCertificate D
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@InvoicerateId
and a.CertificateRequestId = H.RequestId
and A.CertificateRequestId = D.RequestId
and H.RequestId = D.RequestId
and A.SupportingDocID =@InvoiceSupportingDocId
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,D.CertificateId,H.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,S.SupportingDocumentName as ittemtype,1 as Qty,E.Rates,''as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E,tblSupportingDocuments S , tblCertifcateRequestHeader H , tblCertificate D
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@OtherDocumentRateId
and A.CertificateRequestId = H.RequestId
and A.CertificateRequestId = D.RequestId
and H.RequestId = D.RequestId
and A.SupportingDocID !=@InvoiceSupportingDocId
and A.SupportingDocID !='SDID5'
and S.SupportingDocumentId = A.SupportingDocID
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember 
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,D.CertificateId,H.InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,S.SupportingDocumentName as ittemtype,1 as Qty,E.Rates,''as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E,tblSupportingDocuments S, tblUploadBasedCertificateRequest H,tblCertificate D
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@OtherDocumentRateId
and A.CertificateRequestId = H.RequestId
and A.CertificateRequestId = D.RequestId
and H.RequestId = D.RequestId
and A.SupportingDocID !=@InvoiceSupportingDocId
and A.SupportingDocID !='SDID5' -- Attache CO Ignore Request by L A Pradeep
and S.SupportingDocumentId = A.SupportingDocID
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember 
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,A.ApprovedDate,111)as CreatedDate,a.RequestID as CertificateId,'S/D Req' as InvoiceNo,C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,S.SupportingDocumentName as ittemtype,1 as Qty,E.Rates,''as Consignee,'' as Consignor
from tblSupportingDocApproveRequest A, tblCustomer C,tblCustomerApplicableRates E,tblSupportingDocuments S
where A.Status='A' and 
A.CustomerId=C.CustomerId
and A.CustomerId=E.CustomerId
and E.RatesId=@OtherDocumentRateId
and A.CertificateRequestId is null
and A.SupportingDocID !=@InvoiceSupportingDocId
and A.SupportingDocID !='SDID5' -- Attache CO Ignore Request by L A Pradeep
and S.SupportingDocumentId = A.SupportingDocID
and Convert(varchar,A.ApprovedDate,112)>=@FromDate
and Convert(varchar,A.ApprovedDate,112)<=@ToDate
and A.CustomerId like @CustomerId
and NCEMember like @NCEMember 
and C.PaidType like  @Paytype
and A.RequestID not in 
(select DocumentId from tblCancelledCertificate)

union
select Convert(varchar,B.CreatedDate,111)as CreatedDate,ReferenceNo as CertificateId,ExporterInvoiceNo as InvoiceNo,
C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'CO' as ittemtype,1 as Qty,E.Rates,'' as Consignee,'' as Consignor
from tblManualCertificate B, tblCustomer C,tblCustomerApplicableRates E
where 
B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@CertificateRateId
and Convert(varchar,B.CreatedDate,112)>=@FromDate
and Convert(varchar,B.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype
and ItemDescription='C'
and ReferenceNo not in
(select DocumentId from tblCancelledCertificate)

union

select Convert(varchar,B.CreatedDate,111)as CreatedDate,ReferenceNo as CertificateId,ExporterInvoiceNo as InvoiceNo,
C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'Invoice' as ittemtype,1 as Qty,E.Rates,'' as Consignee,'' as Consignor
from tblManualCertificate B, tblCustomer C,tblCustomerApplicableRates E
where 
B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@InvoicerateId
and Convert(varchar,B.CreatedDate,112)>=@FromDate
and Convert(varchar,B.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember
and C.PaidType like  @Paytype 
and ItemDescription='I'
and ReferenceNo not in
(select DocumentId from tblCancelledCertificate)

union
select Convert(varchar,B.CreatedDate,111)as CreatedDate,ReferenceNo as CertificateId,ExporterInvoiceNo as InvoiceNo,
C.CustomerName,'' as PortOfDischarge,
C.ExportSector,NCEMember,'Documents' as ittemtype,1 as Qty,E.Rates,'' as Consignee,'' as Consignor
from tblManualCertificate B, tblCustomer C,tblCustomerApplicableRates E
where 
B.CustomerId=C.CustomerId
and B.CustomerId=E.CustomerId
and E.RatesId=@OtherDocumentRateId
and Convert(varchar,B.CreatedDate,112)>=@FromDate
and Convert(varchar,B.CreatedDate,112)<=@ToDate
and B.CustomerId like @CustomerId
and NCEMember like @NCEMember 
and C.PaidType like  @Paytype
and ItemDescription='O'
and ReferenceNo not in
(select DocumentId from tblCancelledCertificate)
) tmp

GROUP BY CustomerName, YEAR(CreatedDate), MONTH(CreatedDate)
ORDER BY CustomerName, YEAR(CreatedDate), MONTH(CreatedDate)

END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetCountry]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISgetCountry](@CountryCode varchar(5))
AS
BEGIN
	Select * from tblCountry where CountryCode like @CountryCode 
END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetCRequestDetailsByID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISgetCRequestDetailsByID](@RequestID varchar(20))
as Begin

/**
 * Made By : Tharaka
 * Changed date : 12/7/2016
 * Change Done : Removed tblPackageType from query
 * Requested Date : 12/6/2016
 * Version No : 12.1
 * **/

Select

SeqNo, 
RequestId, 
GoodItem, 
ShippingMark,
PackageType,
PackageType as PackageDescription,
SummaryDesc, 
Quantity, 
HSCode



from tblCertificateRequestDetails /**, tblPackageType B**/
/**where A.PackageType = B.PackageType**/
where RequestId = @RequestID

End

GO
/****** Object:  StoredProcedure [dbo].[DCISgetCRequestHeaderDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISgetCRequestHeaderDetails](@RequestID varchar(20))
as begin
select 
RequestId, 
TemplateName,
A.TemplateId,
CustomerName,
RequestDate, 
A.ModifiedDate,
A.ModifiedBy,
A.CreatedDate, 
A.CreatedBy, 
A.Status, 
Consignor,
Consignee,
InvoiceNo,
InvoiceDate, 
CountryName,
A.CountryCode,  
LoadingPort, 
PortOfDischarge,
Vessel, 
PlaceOfDelivery, 
TotalInvoiceValue, 
TotalQuantity,
Telephone as CustomerTelephone,
A.CustomerId,
A.Status,
A.OtherComments,
A.OtherDetails,
U.PersonName,
U.Designation,
A.SealRequired

from tblCertifcateRequestHeader A,tblTemplateHeader B,tblCountry C,tblCustomer D , tblUser U
where A.CustomerId = D.CustomerId
and D.CustomerId = u.CustomerId
and A.CreatedBy = U.UserID
and A.TemplateId = B.TemplateId
and A.CountryCode = C.CountryCode
and RequestId =@RequestID

End









GO
/****** Object:  StoredProcedure [dbo].[DCISgetCRequestSupportingDOC]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISgetCRequestSupportingDOC](@RequestID varchar(20))
as begin

Select
RequestRefNo, 
A.SupportingDocumentId,
B.SupportingDocumentName, 
Remarks, 
UploadedDate, 
UploadedBy, 
UploadedPath, 
UploadSeqNo,
DocumentName,
RequestDate,
SignatureRequired

from tblSupportingDOCUpload A, tblSupportingDocuments B,tblCertifcateRequestHeader C
where a.SupportingDocumentId = B.SupportingDocumentId
and a.RequestRefNo = c.RequestId
and C.RequestId = @RequestID

End


GO
/****** Object:  StoredProcedure [dbo].[DCISgetCuscertificateStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetCuscertificateStatus](@RequestId varchar(20),@Status varchar(2),@CustomerId varchar(20))
AS
BEGIN
select
	 b.RequestId,

b.Status

from tblCertifcateRequestHeader b
where b.Status like @Status   and b.CustomerId=@CustomerId

union

select
 c.RequestId,
	
C.Status

from tblUploadBasedCertificateRequest c
where  c.Status like @Status   and C.CustomerId=@CustomerId


END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetCusTemplateID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create

PROCEDURE[dbo].[DCISgetCusTemplateID]
(

@CustomerId  varchar (20))
AS

BEGIN

select TemplateId from tblCustomerTemplate
where

CustomerId like @CustomerId 





　

END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustIDfrmUserID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetCustIDfrmUserID]@userid varchar(20)
as
begin
select CustomerId,UserID from tblUser
where UserID like @userid 
end










GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerConsignees]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[DCISgetCustomerConsignees](@CustomerID varchar(20))
as Begin
SELECT Consignee, CustomerId, RequestId, SeqNo
FROM tblCustomerRequestReffrence
WHERE CustomerId like @CustomerID
End
GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCustomerDetail]
	(@CustomerId varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblCustomer WHERE CustomerId=@CustomerId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerEmail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetCustomerEmail]
AS
BEGIN
	
	SELECT a.* ,b.PersonName FROM tblCustomerEmail a, tblUser b
	where a.UserID=b.UserID
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerEmails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISgetCustomerEmails](@CustomerID varchar(20))
AS BEGIN
SELECT * FROM tblCustomerEmail
WHERE CustomerId LIKE @CustomerID

END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerExportSector]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCustomerExportSector]
	(@CustomerId varchar(20))
AS
BEGIN
	SELECT
	B.ExportId,B.ExportSector,A.CustomerExportSectorId
	FROM
	tblCustomerExportSector A,tblExportSector B
	WHERE
	CustomerId=@CustomerId
	AND
	B.ExportId=A.ExportSectorId
	AND
	A.Status='Y'
END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerList]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE proc [dbo].[DCISgetCustomerList]
	as begin
	
	SELECT
	A.CustomerName,A.CustomerId,CONVERT(VARCHAR(10),A.CreatedDate,102) AS CreatedDate,A.Address1,A.Address2,A.Address3,
	A.ContactPersonDesignation,A.ContactPersonDirectPhoneNumber,A.ContactPersonEmail,
	A.ContactPersonMobile,A.ContactPersonName,A.CreatedBy,A.Email,A.Fax,
	A.NCEMember,CAST(A.ProductDetails AS NVARCHAR(MAX)) as ProductDetails,A.Telephone,B.TemplateId,C.TemplateName,A.PaidType,'Yes' as IsSVat 
	FROM
	tblCustomer A,tblCustomerTemplate B,tblTemplateHeader C
	WHERE
	A.CustomerId Like '%'
	AND
	B.CustomerId=A.CustomerId
	AND
	B.TemplateId=C.TemplateId
	AND A.IsSVat like '1'

	union

    SELECT
	A.CustomerName,A.CustomerId, CONVERT(VARCHAR(10),A.CreatedDate,102) AS CreatedDate,A.Address1,A.Address2,A.Address3,
	A.ContactPersonDesignation,A.ContactPersonDirectPhoneNumber,A.ContactPersonEmail,
	A.ContactPersonMobile,A.ContactPersonName,A.CreatedBy,A.Email,A.Fax,
	A.NCEMember,CAST(A.ProductDetails AS NVARCHAR(MAX))as ProductDetails,A.Telephone,B.TemplateId,C.TemplateName,A.PaidType,'No' as IsSVat 
	FROM
	tblCustomer A,tblCustomerTemplate B,tblTemplateHeader C
	WHERE
	A.CustomerId Like '%'
	AND
	B.CustomerId=A.CustomerId
	AND
	B.TemplateId=C.TemplateId
	AND A.IsSVat like '0'


	Order By A.CustomerId

	End

GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerRateDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCustomerRateDetails]
	(@CustomerId varchar(20),@status varchar(1))
AS
BEGIN
	SELECT
	A.RateId,A.RateName,B.Rates,C.PaidType
	FROM
	tblRates A,tblCustomerApplicableRates B,tblCustomer C
	WHERE
	A.RateId=B.RatesId
	AND
	B.CustomerId=@CustomerId
	AND
	B.CustomerId=C.CustomerId
	AND
	A.Status=@status
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCustomerRequest] 
	(@Status varchar(10))
AS
BEGIN
	
	SELECT * FROM tblCustomerRequest WHERE status=@Status
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerSVAT]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCustomerSVAT]
	-- Add the parameters for the stored procedure here
	(@CustomerId varchar(20))
AS
BEGIN
	SELECT
	IsSVat
	FROM
	tblCustomer
	WHERE
	CustomerId=@CustomerId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerTax]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetCustomerTax] 
	(@CustomerId varchar(20),@isActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT 
   A.TaxCode,A.CustomerId,A.TaxRegistrationNo,B.TaxName,B.TaxPercentage
    FROM 
   tblCustomerApplicableTax A,tblTax B
   WHERE
    B.TaxCode=A.TaxCode 
	And
	A.CustomerId=@CustomerId
	AND
	A.IsActive=@isActive
	order by
	B.TaxPriority

END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerTemplate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISgetCustomerTemplate](@CustomerID varchar(20))
as begin

select TemplateId ,B.CustomerName,b.Address1,b.Address2,b.Address3,b.Telephone
from tblCustomerTemplate A, tblCustomer b
where B.CustomerId=@CustomerID
and A.CustomerId=B.CustomerId

End 
GO
/****** Object:  StoredProcedure [dbo].[DCISgetCustomerUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE

PROCEDURE[dbo].[DCISgetCustomerUser]
(

@IsActive varchar(1))
AS

BEGIN

select a.PersonName,a.UserID,a.CustomerId,b.CustomerName from tblUser a,tblCustomer b
where

a.CustomerId=b.CustomerId and
 UserGroupID like 'CAdmin'
 and
IsActive like @IsActive
--union
--select PersonName,UserID,CustomerId from tblUser a,
--where


-- UserGroupID like 'CUSTOMER'
-- and
--IsActive like @IsActive

END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetDownloadCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetDownloadCount](@CustomerId varchar(20),@userID varchar(20))
AS
BEGIN
select
(select
	COUNT (a.IsDownloaded)
	from
	tblCertificate a,tblCertifcateRequestHeader b,tblUser c
	where b.CustomerId like @CustomerId and a.RequestId=b.RequestId and b.CreatedBy=c.UserID and c.UserID like @userID  and b.SealRequired='True'     and a.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=a.CertificateId) and a.IsDownloaded ='N' )+

	(select
	COUNT (a.IsDownloaded)
	from
	tblCertificate a,tblUploadBasedCertificateRequest b ,tblUser c
	where b.CustomerId like @CustomerId and a.RequestId=b.RequestId  and b.CreatedBy=c.UserID and c.UserID like @userID and b.SealRequired='True' and a.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=a.CertificateId) and a.IsDownloaded ='N' )
	(select
	COUNT (a.IsDownloaded)
	from
	tblCertificate a,tblEmailBasedCertificateRequest b ,tblUser c
	where b.CustomerId like @CustomerId and a.RequestId=b.RequestId and b.CreatedBy=c.UserID and  c.UserID like @userID and a.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=a.CertificateId) and a.IsDownloaded ='N')
	 


END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetEBCAllMailParameters]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISgetEBCAllMailParameters]
as
Begin
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCR_EMAIL'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRE_PASS'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRM_SERVER'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRM_SMTP'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRM_SMTP_PORT'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRMS_PORT'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRMS_PROTOCOL'
End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetEBCMailParameters]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISgetEBCMailParameters]
as
Begin
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCR_EMAIL'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRE_PASS'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRM_SERVER'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRMS_PORT'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRMS_PROTOCOL'
End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetEBCSendingMailParameters]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISgetEBCSendingMailParameters]
as
Begin
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCR_EMAIL'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRE_PASS'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRM_SMTP'
UNION
SELECT ParameterCode,ParameterValue FROM tblParameter WHERE ParameterCode LIKE 'EBCRM_SMTP_PORT'
End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetEmail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetEmail]
AS
BEGIN
	
	SELECT * FROM tblWaitingEmail
END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetEmailBasedCertificateRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISgetEmailBasedCertificateRequest](@Status varchar(5))
as Begin

Select 
RequestId,  
A.Email, 
A.CustomerId,
CustomerName,
RecivedDate,
A.Status, 
EmailSubject
From tblEmailBasedCertificateRequest A, tblCustomer B
Where A.Status like @Status
and A.CustomerId = B.CustomerId
order by a.CreatedDate

End




GO
/****** Object:  StoredProcedure [dbo].[DCISgetEmailCustomerReqTest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetEmailCustomerReqTest] 
	(@RequestId varchar(20),@CustomerId varchar(20),@Status varchar(3),@StartDate varchar(10),@EndDate varchar(10),@type varchar(10),@InvoiceNo varchar(20))
AS
BEGIN
if(@type like'Emailed')
SELECT
'Emailed' As 'Method'	,A.RequestId,A.CustomerId,A.Status,A.CreatedDate,C.CustomerName,A.ReasonCode,B.InvoiceNo



FROM 
tblCertifcateRequestHeader B, 
	tblEmailBasedCertificateRequest A,
	tblCustomer C

	WHERE 
	A.CustomerId=C.CustomerId
  and A.Status like @Status

and A.RequestId like @RequestId
--and B.InvoiceNo like @InvoiceNo 
and A.CustomerId like @CustomerId
and 
convert(varchar,A.CreatedDate,112)>=@StartDate
and convert(varchar,A.CreatedDate,112)<=@EndDate
order by A.CreatedDate desc

if(@type like'Document')
	Select	
	'Document' as 'Method',
	B.RequestId,B.CustomerId,B.Status,B.RequestDate AS CreatedDate,C.CustomerName,B.RejectReasonCode,'none' as InvoiceNo
	
FROM 
    	
	tblSupportingDocApproveRequest B,
	tblCustomer C

	WHERE 
C.CustomerId=B.CustomerId
 and B.Status like @Status
and B.CustomerId like @CustomerId
--and A.InvoiceNo like @InvoiceNo 
and B.RequestId like @RequestId

--and isnull(b.CertificateRequestId,'')  NOT in
	--(SELECT DocumentId FROM tblCancelledcertificate  )
 and  isnull(b.RequestID,'') NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )

and B.CustomerId like @CustomerId
and 
convert(varchar,B.RequestDate,112)>=@StartDate
and convert(varchar,B.RequestDate,112)<=@EndDate
order by B.RequestDate desc

if(@type like'Uploaded')

	Select
	'Uploaded Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.RejectReasonCode,b.InvoiceNo
	
FROM 
	
	tblUploadBasedCertificateRequest B,
	tblCustomer C

	WHERE 
	 isnull(b.CertificateId,'')  NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )

and
C.CustomerId=B.CustomerId
 and B.Status like @Status
and B.CustomerId like  @CustomerId
and B.RequestId like @RequestId
 
and B.CustomerId like  @CustomerId
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate
order by B.CreatedDate desc

if( @Status = 'P' and @type='Normal'   )

	Select	
	'Normal Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.ReasonCode,b.InvoiceNo
	
FROM 	
	tblCertifcateRequestHeader B,
	tblCustomer C


	WHERE 

C.CustomerId=B.CustomerId

 and B.Status like 'G'

and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
and B.InvoiceNo like @InvoiceNo 
and B.CustomerId like @CustomerId
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate




order by B.CreatedDate desc








if(@type='Normal' and @Status like '%' and @Status != 'P'  )

	Select	
	'Normal Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.ReasonCode,b.InvoiceNo
	
FROM 	
	tblCertifcateRequestHeader B,
	tblCustomer C
--	,tblCertificate d

	WHERE 
--	b.RequestId=d.RequestId and
--isnull(d.CertificateId,'')	 NOT in
--(SELECT DocumentId FROM tblCancelledcertificate  ) 


--	and
C.CustomerId=B.CustomerId

 and B.Status like @Status
 and b.Status !='P'
 and B.status !='A'

and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
and B.InvoiceNo like @InvoiceNo 
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate

union


	Select	
	'Normal Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.ReasonCode,b.InvoiceNo
	
FROM 	
	tblCertifcateRequestHeader B,
	tblCustomer C,
tblCertificate d

	WHERE 
b.RequestId=d.RequestId and
isnull(d.CertificateId,'')	 NOT in
(SELECT DocumentId FROM tblCancelledcertificate  ) 


	and
C.CustomerId=B.CustomerId

 and B.Status like @Status
 and B.status ='A'

and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
and B.InvoiceNo like @InvoiceNo 
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate







order by B.CreatedDate desc

if(@type like 'All' and @Status = '%')
SELECT
'Emailed' As 'Method'	,A.RequestId,A.CustomerId,A.Status,A.CreatedDate,C.CustomerName,A.ReasonCode,'none' as InvoiceNo



FROM  

	tblEmailBasedCertificateRequest A,
	tblCustomer C

	WHERE 
	A.CustomerId=C.CustomerId
  and A.Status like @Status
--and B.InvoiceNo like @InvoiceNo 
and A.RequestId like @RequestId
and A.CustomerId like @CustomerId
and 
convert(varchar,A.CreatedDate,112)>=@StartDate
and convert(varchar,A.CreatedDate,112)<=@EndDate


	Union

Select	
	'Normal Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.ReasonCode,b.InvoiceNo
	
FROM 	
	tblCertifcateRequestHeader B,
	tblCustomer C
--	,tblCertificate D
	

	WHERE 
	--b.RequestId=d.RequestId and
	--isnull(d.CertificateId,'')  NOT in
	--(SELECT  DocumentId FROM tblCancelledcertificate  )
	
	--and
C.CustomerId=B.CustomerId
 and B.Status like @Status
 and b.Status !='P'
 and B.Status !='A'
and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
and B.InvoiceNo like @InvoiceNo 

and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate
union


Select	
	'Normal Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.ReasonCode,b.InvoiceNo
	
FROM 	
	tblCertifcateRequestHeader B,
	tblCustomer C
	,tblCertificate D
	

	WHERE 
	b.RequestId=d.RequestId and
	isnull(d.CertificateId,'')  NOT in
	(SELECT  DocumentId FROM tblCancelledcertificate  )
	
	and
C.CustomerId=B.CustomerId
  and b.Status !='P'
 and B.Status ='A'
and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
and B.InvoiceNo like @InvoiceNo 

and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate






Union
	Select	
	'Document' as 'Method',
	B.RequestId,B.CustomerId,B.Status,B.RequestDate,C.CustomerName,B.RejectReasonCode,'none' as InvoiceNo
	
FROM 
	
	tblSupportingDocApproveRequest B,
	tblCustomer C

	WHERE 
C.CustomerId=B.CustomerId
 and B.Status like @Status
and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
--and  isnull(b.CertificateRequestId,'')  NOT in
--	(SELECT DocumentId FROM tblCancelledcertificate )

	and  isnull(b.RequestID,'')  NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )
--and A.InvoiceNo like @InvoiceNo 
and B.CustomerId like   @CustomerId
and 
convert(varchar,B.RequestDate,112)>=@StartDate
and convert(varchar,B.RequestDate,112)<=@EndDate

union

	Select
	'Uploaded Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.RejectReasonCode,b.InvoiceNo
	
FROM 
	tblCertifcateRequestHeader A,
	tblUploadBasedCertificateRequest B,
	tblCustomer C

	WHERE 
	 isnull(b.CertificateId,'')  NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )
	and
C.CustomerId=B.CustomerId
 and B.Status like  @Status
and B.CustomerId like @CustomerId
--and A.InvoiceNo like @InvoiceNo 
and B.RequestId like @RequestId

and B.CustomerId like @CustomerId
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate
order by CreatedDate desc


if(@type like 'All' and @Status != 'P' and  @Status != '%' )
SELECT
'Emailed' As 'Method'	,A.RequestId,A.CustomerId,A.Status,A.CreatedDate,C.CustomerName,A.ReasonCode,'none' as InvoiceNo



FROM  

	tblEmailBasedCertificateRequest A,
	tblCustomer C

	WHERE 
	A.CustomerId=C.CustomerId
  and A.Status like @Status
--and B.InvoiceNo like @InvoiceNo 
and A.RequestId like @RequestId
and A.CustomerId like @CustomerId
and 
convert(varchar,A.CreatedDate,112)>=@StartDate
and convert(varchar,A.CreatedDate,112)<=@EndDate


	


union


Select	
	'Normal Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.ReasonCode,b.InvoiceNo
	
FROM 	
	tblCertifcateRequestHeader B,
	tblCustomer C
	

	WHERE 
	
C.CustomerId=B.CustomerId
 and B.Status like  @Status
 and B.status !='G'
 and B.status !='P'
 and b.status !='A'
and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
and B.InvoiceNo like @InvoiceNo 

and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate

union

Select	
	'Normal Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.ReasonCode,b.InvoiceNo
	
FROM 	
	tblCertifcateRequestHeader B,
	tblCustomer C,
	tblCertificate d

	
	

	WHERE 
b.RequestId=d.RequestId and
isnull(d.CertificateId,'')	 NOT in
(SELECT DocumentId FROM tblCancelledcertificate  ) 

and
	
C.CustomerId=B.CustomerId
 and B.Status like  @Status
 and B.status !='G'
 and B.status !='P'
 and b.status ='A'
and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
and B.InvoiceNo like @InvoiceNo 

and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate










Union
	Select	
	'Document' as 'Method',
	B.RequestId,B.CustomerId,B.Status,B.RequestDate,C.CustomerName,B.RejectReasonCode,'none' as InvoiceNo
	
FROM 
	
	tblSupportingDocApproveRequest B,
	tblCustomer C

	WHERE 
C.CustomerId=B.CustomerId
 and B.Status like @Status
and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
--and  isnull(b.CertificateRequestId,'')  NOT in
--	(SELECT DocumentId FROM tblCancelledcertificate )

	and  isnull(b.RequestID,'')  NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )
--and A.InvoiceNo like @InvoiceNo 
and B.CustomerId like   @CustomerId
and 
convert(varchar,B.RequestDate,112)>=@StartDate
and convert(varchar,B.RequestDate,112)<=@EndDate

union

	Select
	'Uploaded Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.RejectReasonCode,b.InvoiceNo
	
FROM 
	tblCertifcateRequestHeader A,
	tblUploadBasedCertificateRequest B,
	tblCustomer C

	WHERE 
	 isnull(b.CertificateId,'')  NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )
	and
C.CustomerId=B.CustomerId
 and B.Status like  @Status
and B.CustomerId like @CustomerId
--and A.InvoiceNo like @InvoiceNo 
and B.RequestId like @RequestId

and B.CustomerId like @CustomerId
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate
order by CreatedDate desc



if(@type like'All' and @Status = 'P')
SELECT
'Emailed' As 'Method'	,A.RequestId,A.CustomerId,A.Status,A.CreatedDate,C.CustomerName,A.ReasonCode,'none' as InvoiceNo



FROM  

	tblEmailBasedCertificateRequest A,
	tblCustomer C

	WHERE 
	A.CustomerId=C.CustomerId
  and A.Status like @Status
--and B.InvoiceNo like @InvoiceNo 
and A.RequestId like @RequestId
and A.CustomerId like @CustomerId
and 
convert(varchar,A.CreatedDate,112)>=@StartDate
and convert(varchar,A.CreatedDate,112)<=@EndDate


	Union
	Select	
	'Normal Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.ReasonCode,b.InvoiceNo
	
FROM 	
	tblCertifcateRequestHeader B,
	tblCustomer C
	

	WHERE 
	--b.RequestId=d.RequestId and
	isnull(b.RequestId,'')  NOT in
	(SELECT  DocumentId FROM tblCancelledcertificate  )
	
	and
C.CustomerId=B.CustomerId
 and B.Status like 'G' --@Status
and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
and B.InvoiceNo like @InvoiceNo 
and B.CustomerId like @CustomerId
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate



Union
	Select	
	'Document' as 'Method',
	B.RequestId,B.CustomerId,B.Status,B.RequestDate,C.CustomerName,B.RejectReasonCode,'none' as InvoiceNo
	
FROM 
	
	tblSupportingDocApproveRequest B,
	tblCustomer C

	WHERE 
C.CustomerId=B.CustomerId
 and B.Status like @Status
and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
--and  isnull(b.CertificateRequestId,'')  NOT in
--	(SELECT DocumentId FROM tblCancelledcertificate )

	and  isnull(b.RequestID,'')  NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )
--and A.InvoiceNo like @InvoiceNo 
and B.CustomerId like   @CustomerId
and 
convert(varchar,B.RequestDate,112)>=@StartDate
and convert(varchar,B.RequestDate,112)<=@EndDate

union

	Select
	'Uploaded Certificate' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.RejectReasonCode,b.InvoiceNo
	
FROM 
	tblCertifcateRequestHeader A,
	tblUploadBasedCertificateRequest B,
	tblCustomer C

	WHERE 
	 isnull(b.CertificateId,'')  NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )
	and
C.CustomerId=B.CustomerId
 and B.Status like  @Status
and B.CustomerId like @CustomerId
--and A.InvoiceNo like @InvoiceNo 
and B.RequestId like @RequestId

and B.CustomerId like @CustomerId
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate
order by CreatedDate desc












	





END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetEmailfromReqID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetEmailfromReqID] 
	(@Status varchar(3))
AS
BEGIN

SELECT
	Email
FROM  
	tblEmailBasedCertificateRequest A,
	tblCertificate B
	WHERE 

  B.IsDownloaded like @Status
and A.RequestId =B.RequestId
END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetEmailPendingCertificateReqCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetEmailPendingCertificateReqCount]
AS
BEGIN

select
	COUNT (Status)
	from
	 tblEmailBasedCertificateRequest
	 where status='P' 


END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetEmialCertificateConfig]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[DCISgetEmialCertificateConfig](@CustomerId varchar(20))
AS
BEGIN
select
a.*,

b.CustomerName


from tblEmailCertificateConfig a,tblCustomer b
where a.CustomerId=b.CustomerId and a.CustomerId like @CustomerId

END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetExportSector]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetExportSector](@Status varchar(5))
AS
BEGIN
	Select * from tblExportSector where Status like @Status 
END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetINputInvoiceTotal]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[DCISgetINputInvoiceTotal]( @InvoicNo nvarchar(50))
as
Begin
select Sum(Total) Total from (
select Count(InvoiceNo) as Total
from tblCertifcateRequestHeader
where InvoiceNo like @InvoicNo
union
select Count(InvoiceNo) as Total
from tblUploadBasedCertificateRequest
where InvoiceNo like @InvoicNo
)tmp

End

GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvNoM]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DCISgetInvNoM](@REqNo varchar(20))
AS
BEGIN
	Select RequestNo from tblInvoiceDetail where RequestNo like @REqNo
END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoiceBody]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoiceBody] 
	(@InvoiceNo varchar(20))
AS
BEGIN
	SELECT
	B.RequestNo,C.CertificateId,A.Consignee,A.Consignor,B.UnitCharge,B.CreatedDate
	FROM
	tblCertifcateRequestHeader A ,tblInvoiceDetail B,tblCertificate C
	WHERE
	B.InvoiceNo=@InvoiceNo
	AND
	B.RequestNo=C.RequestId
	AND
	B.RequestNo=A.RequestId
	UNION
	SELECT
	B.RequestNo,C.CertificateId,NULL AS Consignee,D.CustomerName AS Consignor,B.UnitCharge,B.CreatedDate
	FROM
	tblUploadBasedCertificateRequest A ,tblInvoiceDetail B,tblCertificate C,tblCustomer D
	WHERE
	B.InvoiceNo=@InvoiceNo
	AND
	B.RequestNo=C.RequestId
	AND
	B.RequestNo=A.RequestId
	AND
	A.CustomerId=D.CustomerId

	UNION
	SELECT
	B.RequestNo,A.ReferenceNo As CertificateId,NULL AS Consignee,D.CustomerName AS Consignor,B.UnitCharge,B.CreatedDate
	FROM
	tblManualCertificate A ,tblInvoiceDetail B,tblCustomer D
	WHERE
	B.InvoiceNo=@InvoiceNo
	AND
	B.RequestNo=A.ReferenceNo
	AND
	A.CustomerId=D.CustomerId

END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoiceDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoiceDetails] 
	(@Status varchar(3),@StartDate varchar(10),@EndDate varchar(10),@customerId varchar(20)
,@rateId varchar(20))
AS
BEGIN

SELECT
	A.RequestId,A.CustomerId,A.CreatedDate, Consignor,Consignee,D.Rates 

FROM  
	tblCertifcateRequestHeader A, tblCertificate B,
	tblCustomerApplicableRates D
WHERE 
Status=@Status
AND
A.RequestId=B.RequestId
AND
	B.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=B.CertificateId)


and D.RatesId=@rateId
and
A.RequestId
Not in
(
SELECT E.RequestNo  FROM	tblInvoiceDetail E 	Where		E.RequestNo=A.RequestId
)

and 
A.CustomerId=@customerId
AND
D.CustomerId=A.CustomerId
and 
convert(varchar,A.CreatedDate,112)>=@StartDate
and convert(varchar,A.CreatedDate,112)<=@EndDate

UNION

SELECT
	A.RequestId,A.CustomerId,A.CreatedDate,A.CustomerId AS Consignor ,null AS Consignee,D.Rates 

FROM  
	tblUploadBasedCertificateRequest A, tblCertificate B,
	tblCustomerApplicableRates D
WHERE 
Status=@Status


and D.RatesId=@rateId

and
A.RequestId=B.RequestId
AND
	B.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=B.CertificateId)
AND
A.RequestId
Not in
(
SELECT E.RequestNo  FROM	tblInvoiceDetail E 	Where E.RequestNo=A.RequestId
)

and 
A.CustomerId=@customerId
AND
D.CustomerId=A.CustomerId
and 
convert(varchar,A.CreatedDate,112)>=@StartDate
and convert(varchar,A.CreatedDate,112)<=@EndDate


UNION
--newly Add(2016-11-08)----
SELECT
	A.ReferenceNo AS RequestId,A.CustomerId,A.CreatedDate,A.CustomerId AS Consignor ,null AS Consignee,B.Rates 

FROM  
	tblManualCertificate A,
	tblCustomerApplicableRates B
WHERE 
Status='Y'--@Status


and B.RatesId=@rateId
AND
A.ItemDescription like 'C'
AND
	A.ReferenceNo NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.ReferenceNo)
AND
A.ReferenceNo
Not in
(
SELECT E.RequestNo  FROM	tblInvoiceDetail E 	Where E.RequestNo=A.ReferenceNo
)

and 
A.CustomerId=@customerId
AND
B.CustomerId=A.CustomerId
and 
convert(varchar,A.CreatedDate,112)>=@StartDate
and convert(varchar,A.CreatedDate,112)<=@EndDate


END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoiceDuplicateDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoiceDuplicateDetail]
	(@RequestId varchar(20),@InvoiceNo varchar(20))
AS
BEGIN
	SELECT
		InvoiceNo
	FROM
		tblInvoiceDetail
	WHERE
		RequestNo=@RequestId
	AND
		InvoiceNo=@InvoiceNo
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoiceHeader]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoiceHeader]
	(@InvoiceNo varchar(20))
AS
BEGIN
	SELECT 
	A.CreatedDate,B.CustomerName,B.CustomerId,B.Address1,B.Address2,B.Address2,B.Address3,A.FromDate,A.ToDate
	FROM
	tblInvoiceHeader A,tblCustomer B
	WHERE
	A.InvoiceNo=@InvoiceNo
	AND
	B.CustomerId=A.CustomerId
	
	
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoicePrintTime]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoicePrintTime] 
	(@InvoiceNo varchar(20))
AS
BEGIN
	SELECT 
	PrintTime
	FROM
	tblInvoiceHeader
	WHERE
	InvoiceNo=@InvoiceNo

	Update tblInvoiceHeader
	SET
	PrintTime=PrintTime+1
	WHERE
	InvoiceNo=@InvoiceNo
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoiceRateHistory]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoiceRateHistory] 
	(@InvoiceNo varchar(20))
AS
BEGIN
SELECT A.RateValue,A.SupportingDocName,B.RequestDate,B.ApprovedDate,B.DownloadDocName,A.RateId
 FROM tblInvoiceRate A,tblSupportingDocApproveRequest B
WHERE
A.InvoiceNo=@InvoiceNo
AND
B.RequestID=A.SupportingDocName
UNION
SELECT A.RateValue,A.SupportingDocName,B.CreatedDate AS RequestDate,B.IssuedDate AS ApprovedDate,B.ItemDescription AS DownloadDocName,A.RateId
 FROM tblInvoiceRate A,tblManualCertificate B
WHERE
A.InvoiceNo=@InvoiceNo
AND
B.ReferenceNo=A.SupportingDocName

END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoiceReport]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoiceReport] 
	(@startdate varchar(8),@enddate  varchar(8),@customerId varchar(20))
AS
BEGIN
	SELECT  A.InvoiceNo,A.CustomerId,A.CreatedDate,A.FromDate,A.ToDate,InvoiceTotal,A.GrossTotal,B.CustomerName
	 FROM 
	tblInvoiceHeader A,tblCustomer B
	WHERE
	A.CustomerId=B.CustomerId
	AND
	A.CustomerId like @customerId
	AND
	convert(varchar,A.CreatedDate,112)>=@startdate
	AND
	convert(varchar,A.CreatedDate,112)<=@enddate
	
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoiceReportDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoiceReportDetails]
	(@InvoiceNo varchar(20))
AS
BEGIN
	SELECT 
	A.RequestNo,A.UnitCharge	
	From
		tblInvoiceDetail A
	WHERE
		A.InvoiceNo=@InvoiceNo
		
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoiceTax]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoiceTax] 
	(@InvoiceNo varchar(20))
AS
BEGIN

SELECT
	A.TaxCode, A.Amount,A.TaxPercentage,B.TaxName
FROM
	tblInvoiceTax A,tblTax B
WHERE
	A.TaxCode=B.TaxCode
AND
	A.InvoiceNo=@InvoiceNo
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetInvoiceTaxValues]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetInvoiceTaxValues]
	(@InvoiceNo varchar(20))
AS
BEGIN
	SELECT 
	B.IsTaxInvoice,B.GrossTotal,B.InvoiceTotal
	FROM
	tblInvoiceHeader B
	WHERE
	B.InvoiceNo=@InvoiceNo
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetISCASHORCRETID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISgetISCASHORCRETID]
	(@CustometId varchar(20))
AS
BEGIN
	SELECT
	PaidType
	FROM
	tblCustomer
	Where
	CustomerId=@CustometId
END


GO
/****** Object:  StoredProcedure [dbo].[DCISgetLeveledUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE

PROCEDURE[dbo].[DCISgetLeveledUser]
(

@UserId varchar (20),@UserGroupID varchar (20))
AS

BEGIN

select * from tblUser
where
UserID like @UserID
and
UserGroupID like @UserGroupID
　

END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetManualCertifiLIst]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISgetManualCertifiLIst] 
	(@Status varchar(1),@CustomerID varchar(20),@RefNo varchar(20))
AS
BEGIN
	SELECT 
	a.ReferenceNo,a.IssuedDate,a.ExporterInvoiceNo,a.ItemDescription,b.CustomerName,b.CustomerId,'No' as RequestNo
	FROM tblManualCertificate a,tblCustomer b

	WHERE
	a.ReferenceNo NOT in
	(SELECT RequestNo FROM tblInvoiceDetail  )and

	a.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )and
	a.Status like @Status and a.CustomerId=b.CustomerId and a.CustomerId like @CustomerID and a.ReferenceNo like @RefNo

	union


		SELECT 
	a.ReferenceNo,a.IssuedDate,a.ExporterInvoiceNo,a.ItemDescription,b.CustomerName,b.CustomerId,'Yes' as RequestNo
	FROM tblManualCertificate a,tblCustomer b,tblInvoiceDetail C

	WHERE
	C.RequestNo=a.ReferenceNo and 
	a.ReferenceNo NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )and
	a.Status like @Status and a.CustomerId=b.CustomerId and a.CustomerId like @CustomerID and a.ReferenceNo like @RefNo

END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetMemberRate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetMemberRate]
	(@member varchar(1),@status varchar(1))
AS
BEGIN
SELECT
A.RateValue,A.RateID,B.RateName
FROM
tblMemberRates A,tblRates B
WHERE
A.Member=@member
AND
A.RateID=B.RateId
AND
B.status=@status
END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetMemberStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetMemberStatus]
	(@CustometId varchar(20))
AS
BEGIN
	SELECT
	NCEMember
	FROM
	tblCustomer
	Where
	CustomerId=@CustometId
END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetNCEContactPersonDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetNCEContactPersonDetail]
	
AS
BEGIN
	SELECT A.Email,A.FaxNo,A.Name,A.ImageUrls As TelephoneNo,A.WebAddress FROM tblOwnerDetails A WHERE OwnerId='3'
END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetNotSendEmailCertificates]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[DCISgetNotSendEmailCertificates]
AS BEGIN
SELECT A.RequestId,Email, CustomerId,Status,IsSend, A.CertificateId, B.CertificatePath 
FROM tblEmailBasedCertificateRequest A, tblCertificate B
WHERE A.CertificateId = B.CertificateId
AND Status LIKE 'A'
AND IsSend like 'N'

END


GO
/****** Object:  StoredProcedure [dbo].[DCISgetOtherTax]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetOtherTax] 
	(@CustomerId varchar(20),@taxCode varchar(20))
AS
BEGIN
	SELECT *
	FROM tblCustomerApplicableTax
	WHERE
	CustomerId=@CustomerId
	AND
	TaxCode=@taxCode

END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetOwnerDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetOwnerDetail]
	
AS
BEGIN
	SELECT * FROM
	tblOwnerDetails

END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetOwnerDetailordered]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetOwnerDetailordered](@OwnerId varchar(50))
	
AS
BEGIN
	SELECT OrganizationName,Address1,Address2,Address3,TelephoneNo,FaxNo,Email,WebAddress,ImageUrls,Name FROM
	tblOwnerDetails

	where OwnerId like @OwnerId

END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetPackageType]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shirandi Ekanayake
-- Create date: 30-05-2016
-- Description:	To Retrieve Package Type
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetPackageType](@PackageType varchar(20))
AS
BEGIN
	Select * from tblPackageType 
	where isactive like 'Y'
	and PackageType like @PackageType 
END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetPackageTypen]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetPackageTypen](@PackageType varchar(20),@IsActive varchar(2) )
AS
BEGIN
	Select * from tblPackageType where PackageType like @PackageType and

IsActive like @IsActive
END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetParameaterValues]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetParameaterValues]
	(@ParameterDis varchar(150))
AS
BEGIN
	SELECT 
	* 
	FROM
	tblParameter
	WHERE
	ParameterCode=@ParameterDis
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetPendingCertificateReqCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetPendingCertificateReqCount]
AS
BEGIN

select
	COUNT (H.Status+W.Status)
	from
	 tblWebBasedCertificateRequest W , tblCertifcateRequestHeader H , tblCustomer C ,tblCountry D
Where W.RequestId = H.RequestId
And H.CustomerId = C.CustomerId
And D.CountryCode = H.CountryCode
And H.Status = 'G'
And W.Status = 'P'


	 


END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetPendingSDocApprovals]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISgetPendingSDocApprovals](@Status varchar(5),@CustomerID varchar(20))
as Begin
SELECT RequestID, SupportingDocID,B.SupportingDocumentName, A.CustomerID,C.CustomerName, RequestDate, RequestBy, A.Status, UploadPath, UploadDocName
FROM tblSupportingDocApproveRequest A, tblSupportingDocuments B, tblCustomer C
WHERE A.CustomerID = C.CustomerId
AND A.SupportingDocID = B.SupportingDocumentId
AND A.Status LIKE @Status
AND A.CustomerID LIKE @CustomerID
ORDER BY A.RequestDate

End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetPendingUBasedCertRequst]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISgetPendingUBasedCertRequst](@CustomerID varchar(20))
as Begin
SELECT RequestId, A.CustomerId,B.CustomerName, RequestDate, A.Status, A.CreatedDate, A.CreatedBy, A.UploadPath,A.SealRequired,
'YES' as CollectionType
FROM  tblUploadBasedCertificateRequest A , tblCustomer B
WHERE A.CustomerId = B.CustomerId
AND A.Status like 'P'
and A.CustomerId like @CustomerID
and A.SealRequired like 'True'

union

SELECT RequestId, A.CustomerId,B.CustomerName, RequestDate, A.Status, A.CreatedDate, A.CreatedBy, A.UploadPath,A.SealRequired,
'NO' as CollectionType
FROM  tblUploadBasedCertificateRequest A , tblCustomer B
WHERE A.CustomerId = B.CustomerId
AND A.Status like 'P'
and A.CustomerId like @CustomerID
and A.SealRequired like 'False'

ORDER BY A.CreatedDate

End


GO
/****** Object:  StoredProcedure [dbo].[DCISgetPendingUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetPendingUser]
	(@IsActive varchar(1))
AS
BEGIN
	
	SELECT
		A.UserID,A.PersonName,B.GroupName
	FROM
		tblUserRequest A,tblUserGroup B
	WHERE
		A.Status=@IsActive
		AND
		A.UserGroupID=B.GroupId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetPendingUserRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetPendingUserRequest]
	(@IsActive varchar(1))
AS
BEGIN
	
	SELECT
		A.Password,A.UserRequestId,A.UserId,A.PersonName,B.GroupId,B.GroupName,C.CustomerName,C.CustomerId
	FROM
		tblUserRequest A,tblUserGroup B,tblCustomer C
	WHERE
		A.Status=@IsActive
		AND
		A.UserGroupID=B.GroupId
		AND
		C.CustomerId=A.CustomerId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetPendingWebbasedCertificateDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[DCISgetPendingWebbasedCertificateDetails] (@CustomerID varchar(20))
as begin
Select 
IndexNo, 
W.RequestId, 
CertificatePath, 
CertificateName,
C.CustomerName, 
W.CreatedDate, 
H.RequestDate, 
H.Consignee, 
H.Consignor,
H.InvoiceNo,
D.CountryName,
H.InvoiceDate,
H.TotalInvoiceValue,
H.TotalQuantity, 
W.Status,
H.TemplateId
From tblWebBasedCertificateRequest W , tblCertifcateRequestHeader H , tblCustomer C ,tblCountry D
Where W.RequestId = H.RequestId
And H.CustomerId = C.CustomerId
And D.CountryCode = H.CountryCode
And H.Status = 'G'
And W.Status = 'P'
And H.CustomerId like @CustomerID

order by H.RequestDate

End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetRandomID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[DCISgetRandomID](@useriD varchar(20))

AS
BEGIN

	SELECT RandomID FROM tblUser  where UserID=@useriD
END

GO
/****** Object:  StoredProcedure [dbo].[DCISgetRejectReasons]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetRejectReasons](@RejectCode varchar(20))
AS
BEGIN
	Select * from tblRejectReasons where RejectCode like @RejectCode and IsActive like 'y'
END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetRejectReasonsCategory]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetRejectReasonsCategory](@RejectReasonsCategory varchar(20))
AS
BEGIN
	Select * from tblRejectReasonCategory where RejectReasonsCategory like @RejectReasonsCategory
END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetRejectReasonsn]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetRejectReasonsn](@RejectCode varchar(20),@IsActive varchar(20))
AS
BEGIN
	Select * from tblRejectReasons where RejectCode like @RejectCode and   IsActive like @IsActive
END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetRepotDetailsASC]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetRepotDetailsASC]
	(@customerId varchar(20),@status varchar(1),@start varchar(8),@end varchar(8))
AS
BEGIN
	SELECT
	A.CertificateId,E.NCEMember,B.PortOfDischarge,B.InvoiceNo,
	B.Consignee,A.CreatedBy,E.PaidType,B.TotalInvoiceValue,E.CustomerName,B.RequestId,A.CreatedDate,B.RequestDate
	from 
 tblCertificate A,tblCertifcateRequestHeader B,tblCustomer E

 where A.RequestId=B.RequestId
 and B.Status=@status
 and B.CustomerId like @customerId
 and B.CustomerId=E.CustomerId

 and 

	A.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.CertificateId)
and
convert(varchar,A.CreatedDate,112)>=@start
  and convert(varchar,A.CreatedDate,112)<=@end
 -- order by E.CustomerName ASC

 UNION

  	SELECT
	A.CertificateId,E.NCEMember,Null AS PortOfDischarge ,A.InvoiceNo,
	Null AS Consignee,A.CreatedBy,E.PaidType,NULL AS TotalInvoiceValue,E.CustomerName,A.RequestId,A.CreatedDate,A.RequestDate
	from 
 tblUploadBasedCertificateRequest A ,tblCustomer E,tblCertificate B

 where 
  A.Status=@status
 and A.CustomerId like @customerId
 and A.CustomerId=E.CustomerId
 AND B.RequestId=A.RequestId
 AND
	A.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.CertificateId)

 and convert(varchar,B.CreatedDate,112)>=@start
  and convert(varchar,B.CreatedDate,112)<=@end

  UNION

  	SELECT
	A.ReferenceNo,E.NCEMember,Null AS PortOfDischarge ,A.ExporterInvoiceNo,
	Null AS Consignee,A.CreatedBy,E.PaidType,NULL AS TotalInvoiceValue,E.CustomerName,'Manual Co' As RequestId,A.CreatedDate,null As RequestDate
	from 
 tblManualCertificate A ,tblCustomer E

 where 
  A.Status like 'Y'
 and A.CustomerId like @customerId
 and A.CustomerId=E.CustomerId
 AND
	A.ReferenceNo NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.ReferenceNo)

 and convert(varchar,A.CreatedDate,112)>=@start
  and convert(varchar,A.CreatedDate,112)<=@end

   order by E.CustomerName ASC

END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetRepotDetailsDESC]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetRepotDetailsDESC]
	(@customerId varchar(20),@status varchar(1),@start varchar(8),@end varchar(8))
AS
BEGIN
	SELECT
	A.CertificateId,E.NCEMember,B.PortOfDischarge,B.InvoiceNo,
	B.Consignee,A.CreatedBy,E.PaidType,B.TotalInvoiceValue,E.CustomerName,B.RequestId,A.CreatedDate,b.RequestDate
	from 
 tblCertificate A,tblCertifcateRequestHeader B,tblCustomer E

 where A.RequestId=B.RequestId
 and B.Status=@status
 and B.CustomerId like @customerId
 and B.CustomerId=E.CustomerId
 AND
	A.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.CertificateId)

 and convert(varchar,A.CreatedDate,112)>=@start
  and convert(varchar,A.CreatedDate,112)<=@end
 -- order by E.CustomerName ASC

 UNION

  	SELECT
	A.CertificateId,E.NCEMember,Null AS PortOfDischarge ,A.InvoiceNo,
	Null AS Consignee,A.CreatedBy,E.PaidType,NULL AS TotalInvoiceValue,E.CustomerName,A.RequestId,B.CreatedDate,A.RequestDate
	from 
 tblUploadBasedCertificateRequest A ,tblCustomer E,tblCertificate B

 where 
  A.Status=@status
 and A.CustomerId like @customerId
 and A.CustomerId=E.CustomerId
 And B.RequestId=A.RequestId
 AND
	A.CertificateId NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.CertificateId)

 and convert(varchar,B.CreatedDate,112)>=@start
  and convert(varchar,B.CreatedDate,112)<=@end


   UNION

  	SELECT
	A.ReferenceNo,E.NCEMember,Null AS PortOfDischarge ,A.ExporterInvoiceNo,
	Null AS Consignee,A.CreatedBy,E.PaidType,NULL AS TotalInvoiceValue,E.CustomerName,'Manual Co' As RequestId,A.CreatedDate,null AS RequestDate
	from 
 tblManualCertificate A ,tblCustomer E

 where 
  A.Status like 'Y'
 and A.CustomerId like @customerId
 and A.CustomerId=E.CustomerId
 AND
	A.ReferenceNo NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.ReferenceNo)

 and convert(varchar,A.CreatedDate,112)>=@start
  and convert(varchar,A.CreatedDate,112)<=@end

  order by A.CreatedDate DESC
END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetRequestAdminName]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetRequestAdminName]
	
 (@AdminUserId varchar(20),@status varchar(10))
AS
BEGIN
	
	SELECT 
		AdminUserId AS 'ID'
	FROM 
		tblCustomerRequest
	WHERE 
		AdminUserId=@AdminUserId
		And
		Status !=@status
		UNION
		SELECT
		UserID AS 'ID'
		FROM
		tblUser
		WHERE UserID=@AdminUserId

	
		 
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetRequestDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetRequestDetails]
	-- Add the parameters for the stored procedure here
	(@RequestId varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT  A.Name,A.Telephone,A.Email,A.Address1,A.Address2,A.Address3,A.Fax,A.SVat,A.TemplateId,
            A.AdminUserId,A.AdminPassword,A.ContactPersonName,A.ContactPersonDesignation,
			A.ContactPersonDirectPhoneNumber,A.ContactPersonMobile,A.ContactPersonEmail,A.ProductDetails,
			A.NCEMember,A.AdminName
			--,B.RegistrationLetterPath,B.RequestLetterPath
			 FROM
	 tblCustomerRequest A
	 --,tblCustomerRegistartionFiles B
	 WHERE 
	 A.RequestId=@RequestId
	 --AND
	 --A.RequestId=B.RequestId

END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetRequestedUserId]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetRequestedUserId](@userid varchar(20))
as
begin
select UserId from tblUserRequest
where UserId like @userid
end











GO
/****** Object:  StoredProcedure [dbo].[DCISgetRequestExportSector]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetRequestExportSector] 
(@requestNo varchar(20))
AS
BEGIN
	SELECT
	B.ExportSector
	 FROM
	tblCustomerExportSector A,tblExportSector B
	WHERE
	RequestNo=@requestNo
	AND
	A.ExportSectorId=B.ExportId
END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetRequestID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetRequestID](@RequestId varchar(20))
AS
BEGIN
	Select a.RequestId,CertificateName,a.CertificatePath,IsValid,IsDownloaded,ExpiryDate,b.CustomerName,a.CreatedDate,c.RequestDate,e.UserID,c.CreatedBy from tblCertificate a,tblCustomer b,tblCertifcateRequestHeader c ,tblCancelledcertificate D, tblUser E
	where a.RequestId like @RequestId and a.RequestId=c.RequestId and c.CustomerId=b.CustomerId and a.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate ) and e.CustomerId=c.CustomerId and e.UserGroupID='CADMIN'

	

	union
	Select a.RequestId,CertificateName,a.CertificatePath,IsValid,IsDownloaded,ExpiryDate,b.CustomerName,a.CreatedDate,c.RequestDate,E.UserID,c.CreatedBy from tblCertificate a,tblCustomer b,tblUploadBasedCertificateRequest c ,tblCancelledcertificate D,tblUser E
	where a.RequestId like @RequestId and a.RequestId=c.RequestId and c.CustomerId=b.CustomerId and a.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate ) and E.CustomerId=b.CustomerId  and e.UserGroupID='CADMIN'
	union

	Select a.RequestId,CertificateName,a.CertificatePath,IsValid,IsDownloaded,ExpiryDate,b.CustomerName,a.CreatedDate,c.RecivedDate as RequestDate,E.UserID,c.CreatedBy from tblCertificate a,tblCustomer b,tblEmailBasedCertificateRequest c ,tblCancelledcertificate D,tblUser E
	where a.RequestId like @RequestId and a.RequestId=c.RequestId and c.CustomerId=b.CustomerId and a.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate ) and e.CustomerId=b.CustomerId and e.UserGroupID='CADMIN'
	order by RequestDate desc

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetRequestIDUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetRequestIDUser](@RequestId varchar(20),@CustomerId varchar(20),@userid varchar(20))
AS
BEGIN
select
a.RequestId,
a.CertificateId,
a.CertificateName,
a.CertificatePath,
a.IsValid,
a.IsDownloaded,
a.ExpiryDate,
c.CustomerName,
b.RequestDate,
a.CreatedDate,
d.UserID,
b.CreatedBy,
b.SealRequired,
d.PersonName,
d.Designation,
b.InvoiceNo,
b.Consignor,
b.Consignee,
a.CreatedBy as created 

from tblCertificate a,tblCertifcateRequestHeader b,tblCustomer c,tblUser d
where a.RequestId = b.RequestId and b.CustomerId like @CustomerId and b.CustomerId=c.CustomerId and a.RequestId like @RequestId and a.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate ) and c.CustomerId=d.CustomerId and b.CreatedBy=d.UserID --d.UserGroupID='CADMIN'
	and d.UserID like @userid 
union


select
a.RequestId,
a.CertificateId,
a.CertificateName,
a.CertificatePath,
a.IsValid,
a.IsDownloaded,
a.ExpiryDate,
c.CustomerName,
b.RequestDate,
a.CreatedDate,
d.UserID,
b.CreatedBy,
b.SealRequired,
d.PersonName,
d.Designation,
b.InvoiceNo,
'Not Given' as Consignor,
'Not Given' as Consignee,
a.CreatedBy as created


from tblCertificate a,tblUploadBasedCertificateRequest b,tblCustomer c ,tblUser d
where a.RequestId = b.RequestId and b.CustomerId like @CustomerId  and b.CustomerId=c.CustomerId and a.RequestId like @RequestId and a.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate ) and c.CustomerId=d.CustomerId and b.CreatedBy=d.UserID --d.UserGroupID='CADMIN'
and d.UserID like @userid
union

select
a.RequestId,
a.CertificateId,
a.CertificateName,
a.CertificatePath,
a.IsValid,
a.IsDownloaded,
a.ExpiryDate,
c.CustomerName,
b.CreatedDate as RequestDate,
a.CreatedDate,
d.UserID,
b.CreatedBy,
'No' as SealRequired,
d.PersonName,
d.Designation,
'No' as InvoiceNo,
'Not Given' as Consignor,
'Not Given' as Consignee,
a.CreatedBy as created


from tblCertificate a,tblEmailBasedCertificateRequest b,tblCustomer c ,tblUser d
where a.RequestId = b.RequestId and b.CustomerId like @CustomerId and b.CustomerId=c.CustomerId and a.RequestId like @RequestId and a.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate ) and c.CustomerId=d.CustomerId and b.CreatedBy=d.UserID--d.UserGroupID='CADMIN'
and d.UserID like @userid
order by CreatedDate desc


END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetRequestIDUserdate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetRequestIDUserdate](@RequestId varchar(20),@CustomerId varchar(20),@StartDate  varchar(10),@EndDate varchar(10),@CertificateId varchar(10),@sealrequired varchar(10),@InvoiceNo varchar (20))
AS
BEGIN
select
a.RequestId,
a.CertificateId,
a.CertificateName,
a.CertificatePath,
a.IsValid,
a.IsDownloaded,
a.ExpiryDate,
c.CustomerName,
b.RequestDate,
a.CreatedDate,
d.UserID,
b.CreatedBy,
b.SealRequired,
d.PersonName,
d.Designation,
b.InvoiceNo,
b.Consignor,
b.Consignee,
a.CreatedBy as created

from tblCertificate a,tblCertifcateRequestHeader b,tblCustomer c,tblUser d
where a.RequestId = b.RequestId and b.CustomerId like @CustomerId and b.CustomerId=c.CustomerId and a.RequestId like @RequestId and a.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate ) and c.CustomerId=d.CustomerId and b.CreatedBy=d.UserID --d.UserGroupID='CADMIN'
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate
and a.CertificateId like @CertificateId 
and b.SealRequired like @sealrequired
and b.InvoiceNo like @InvoiceNo
union


select
a.RequestId,
a.CertificateId,
a.CertificateName,
a.CertificatePath,
a.IsValid,
a.IsDownloaded,
a.ExpiryDate,
c.CustomerName,
b.RequestDate,
a.CreatedDate,
d.UserID,
b.CreatedBy,
b.SealRequired,
d.PersonName,
d.Designation,
b.InvoiceNo,
'Not Given' as Consignor,

'Not Given' as Consignee,
a.CreatedBy as created

from tblCertificate a,tblUploadBasedCertificateRequest b,tblCustomer c ,tblUser d
where a.RequestId = b.RequestId and b.CustomerId like @CustomerId  and b.CustomerId=c.CustomerId and a.RequestId like @RequestId and a.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate ) and c.CustomerId=d.CustomerId and b.CreatedBy=d.UserID --d.UserGroupID='CADMIN'
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate
and a.CertificateId like @CertificateId 
and b.SealRequired like @sealrequired
and b.InvoiceNo like @InvoiceNo
union

select
a.RequestId,
a.CertificateId,
a.CertificateName,
a.CertificatePath,
a.IsValid,
a.IsDownloaded,
a.ExpiryDate,
c.CustomerName,
b.CreatedDate as RequestDate,
a.CreatedDate,
d.UserID,
b.CreatedBy,
'No' as SealRequired,
d.PersonName,
d.Designation,
'No' as InvoiceNo,
'Not Given' as Consignor,
'Not Given' as Consignee,
a.CreatedBy as created

from tblCertificate a,tblEmailBasedCertificateRequest b,tblCustomer c ,tblUser d
where a.RequestId = b.RequestId and b.CustomerId like @CustomerId and b.CustomerId=c.CustomerId and a.RequestId like @RequestId and a.CertificateId NOT in
	(SELECT DocumentId FROM tblCancelledcertificate ) and c.CustomerId=d.CustomerId and b.CreatedBy=d.UserID--d.UserGroupID='CADMIN'
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate
and a.CertificateId like @CertificateId 

order by CreatedDate desc


END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetRowCertificateDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[DCISgetRowCertificateDetails](@RequestId varchar(20))
as begin
SELECT SeqNo,RequestId, GoodDetails, QuantityDetails, HSCodeDetails, CreatedDate, CreatedBy
FROM tblRowCertificateRequestDetails
WHERE RequestId = @RequestId

END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetSDSignatureCordinates]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[DCISgetSDSignatureCordinates](@SupportingDocID varchar(20))
as Begin
select  
SupportingDocId, LLXcordinates, LLYcordinates, URXcordinates, URYcordinates
from tblSupportingDocumentConfig
where SupportingDocId = @SupportingDocID

End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetSelectedUserGroup]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: Shirandi Ekanayake

-- Create date: 2016/05/25

-- Description: To Retrieve User Group Information

-- =============================================

create 

PROCEDURE[dbo].[DCISgetSelectedUserGroup]
(

@GroupId varchar (20),@IsActive varchar(1))
AS

BEGIN

select * from tblUserGroup
where

GroupId IN ('CUSTOMER','CADMIN')
and

IsActive like @IsActive
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetSequence]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shirandi Ekanayake
-- Create date: 30-05-2016
-- Description:	To Retrieve the Next SequenceNo
-- =============================================

CREATE PROCEDURE [dbo].[DCISgetSequence](@SequenceName varchar(50)) AS

select SequesnceValue from tblSequence
where SequenceName=@SequenceName;




update tblSequence
set  SequesnceValue=SequesnceValue+1
where SequenceName=@SequenceName;







GO
/****** Object:  StoredProcedure [dbo].[DCISgetSigLevelNameandUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetSigLevelNameandUser](@UserId varchar(20),@TemplateId varchar(20),@IsActive varchar(2))
as
begin
select UserId,TemplateId from tblSignatureLevels
where UserId  like @UserId and TemplateId like @TemplateId and IsActive like @IsActive
end










GO
/****** Object:  StoredProcedure [dbo].[DCISgetSignatoryUsers]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetSignatoryUsers](@UserGroup varchar(20))
as begin

select UserID,PersonName + '    [ User ID : ' + UserID +' ]' as PersonName from tblUser 
where UserGroupID like @UserGroup
and IsActive = 'Y'

end




GO
/****** Object:  StoredProcedure [dbo].[DCISgetSignatureCordinates]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISgetSignatureCordinates](@CustomerId varchar(20))
as Begin
select  
CustomerId, LLXcordinates, LLYcordinates, URXcordinates, URYcordinates
from tblEmailCertificateConfig
where CustomerId = @CustomerId

End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetSignatureLevelID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Shirandi Ekanayake
-- Create date: 30-05-2016
-- Description:	To Retrieve Package Type
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetSignatureLevelID](@LevelID varchar(20))
AS
BEGIN
	Select LevelID from tblSignatureLevelHeader where LevelID like @LevelID 
END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetSiqnatureLevels]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: Shirandi Ekanayake

-- Create date: 2016/05/25

-- Description: To Retrieve User Group Information

-- =============================================

CREATE

PROCEDURE[dbo].[DCISgetSiqnatureLevels]
(

@UserId varchar (20),@LevelId varchar(20),@Isactive varchar(2))
AS

BEGIN

select UserId,LevelId,TemplateId,Isactive from tblSignatureLevels
where

UserId like @UserID
and Isactive like @Isactive

　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetStatementCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetStatementCount]
	(@StartDate varchar(10),@EndDate varchar(10),@CustomerId varchar(20))
AS
BEGIN
	SELECT InvoiceNo,FromDate,ToDate,'In' AS 'State' FROM tblInvoiceHeader
	WHERE
	CustomerId=@CustomerId
	AND
	 convert(varchar,FromDate,112)>=@StartDate
	AND
	 convert(varchar,ToDate,112)<=@EndDate
	 UNION
	 SELECT InvoiceNo,FromDate,ToDate,'Befour' AS 'State' FROM tblInvoiceHeader
	WHERE
	CustomerId=@CustomerId
	AND
	 convert(varchar,FromDate,112)<@StartDate
	AND
	 convert(varchar,ToDate,112)>@EndDate
	 UNION
	  SELECT InvoiceNo,FromDate,ToDate,'After' AS 'State' FROM tblInvoiceHeader
	WHERE
	CustomerId=@CustomerId
	AND
	 convert(varchar,FromDate,112)>@StartDate
	AND
	 convert(varchar,ToDate,112)>@EndDate

	

END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetstatusInvoiceData]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetstatusInvoiceData] 
	(@RequestId varchar(20),@CustomerId varchar(20),@Status varchar(3),@StartDate varchar(10),@EndDate varchar(10),@type varchar(10),@InvoiceNo varchar(20))
AS
BEGIN
Select	
	'Normal' as 'Method',	
	 B.RequestId,B.CustomerId,B.Status,B.CreatedDate,C.CustomerName,B.ReasonCode
	
FROM 	
	tblCertifcateRequestHeader B,
	tblCustomer C

	WHERE 
	
C.CustomerId=B.CustomerId
 and B.Status like @Status
and B.CustomerId like @CustomerId
and B.RequestId like @RequestId
and B.InvoiceNo like @InvoiceNo 
and B.CustomerId like @CustomerId
and 
convert(varchar,B.CreatedDate,112)>=@StartDate
and convert(varchar,B.CreatedDate,112)<=@EndDate

order by B.CreatedDate desc





END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupDocConfig]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create  PROCEDURE [dbo].[DCISgetSupDocConfig](@SupDocId varchar(20))
AS
BEGIN
select
a.*,

b.SupportingDocumentName


from tblSupportingDocumentConfig a,tblSupportingDocuments b
where a.SupportingDocId=b.SupportingDocumentId and a.SupportingDocId like @SupDocId

END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupDocID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISgetSupDocID](@Certid  varchar(20))
AS
BEGIN
	Select DownloadPath,RequestID,DownloadDocName,SupportingDocID from tblSupportingDocApproveRequest where CertificateRequestId like @Certid 
	and isnull(RequestID,'') NOT in
	(SELECT DocumentId FROM tblCancelledcertificate  )
END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupDocIDforTemplate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE

PROCEDURE[dbo].[DCISgetSupDocIDforTemplate]
(

@SupportingDocumentId varchar (20))
AS

BEGIN

select SupportingDocumentId from tblSupportingDocuments
where

SupportingDocumentId like @SupportingDocumentId



　

END










GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupDocName]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetSupDocName]
(

@SupportingDocumentId  varchar (20))
AS
BEGIN
	
	SELECT a.SupportingDocumentName FROM tblSupportingDocuments a,tblSupportingDocApproveRequest b
	where a.SupportingDocumentId=b.SupportingDocID and b.RequestID=@SupportingDocumentId 
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupDocNameforTemplate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE

PROCEDURE[dbo].[DCISgetSupDocNameforTemplate]
(

@SupportingDocumentName varchar (20))
AS

BEGIN

select SupportingDocumentName,SupportingDocumentId from tblSupportingDocuments
where

SupportingDocumentName like @SupportingDocumentName



　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportDocument]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: Shirandi Ekanayake

-- Create date: 2016/05/25

-- Description: To Retrieve User Group Information

-- =============================================

CREATE

PROCEDURE[dbo].[DCISgetSupportDocument]
(

@SupportingDocumentId varchar(20),@CreatedBy varchar(50))
AS

BEGIN

select * from tblSupportingDocuments
where

SupportingDocumentId like @SupportingDocumentId
and

CreatedBy like @CreatedBy
　

END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportDocumentConfig]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE

PROCEDURE[dbo].[DCISgetSupportDocumentConfig]
(

@SupportingDocumentId varchar(20))
AS

BEGIN

select a.SupportingDocumentName,b.* from tblSupportingDocuments a, tblSupportingDocumentConfig b
where

a.SupportingDocumentId =b.SupportingDocId

　

END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportDocumentn]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE

PROCEDURE[dbo].[DCISgetSupportDocumentn]
(

@SupportingDocumentId varchar(20),@IsActive varchar(2))
AS

BEGIN

select * from tblSupportingDocuments
where

SupportingDocumentId like @SupportingDocumentId
and

IsActive like @IsActive
　

END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportingDOCforRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetSupportingDOCforRequest](@UserId varchar(20),@TemplateID varchar(20))
as Begin

select b.SupportingDocumentId,b.SupportingDocumentName, A.TemplateId, d.IsMandatory

from tblCustomerTemplate A,tblSupportingDocuments B ,tblCustomer c, tblTemplateSupportingDocument d,tblUser e
where A.TemplateId = d.TemplateId
and d.SupportingDocumentId = b.SupportingDocumentId
and A.CustomerId = e.CustomerId
and A.CustomerId=C.CustomerId
and b.IsActive LIKE 'Y'
and d.IsActive LIKE 'Y'
and  A.TemplateId = @TemplateID
and e.UserID=@UserId
end



GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportingDocRejectResons]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetSupportingDocRejectResons]
as
Begin
Select RejectCode, ReasonName, Category, IsActive
from tblRejectReasons
where Category like 'SUPPORTDOC'
and IsActive like 'Y'

End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportingDocumentDown]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetSupportingDocumentDown](@Status varchar(20),@requestID varchar(20),@reqstID varchar(20))
AS
BEGIN
select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
'SD Request'CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,
'Not Given' as Consignor,

'Not Given' as Consignee,

'Not Given' as InvoiceNo,
'SD Request' as CertificateId


from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e

where b.SupportingDocID=c.SupportingDocumentId and

b.CertificateRequestId is null and 
 b.Status like @Status and b.CustomerID like @requestID and b.RequestID like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )


 union

  select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
b.CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,
'Not Given' as Consignor,

'Not Given' as Consignee,
f.InvoiceNo,
ct.CertificateId




from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e,tblUploadBasedCertificateRequest f, tblCertificate ct

where b.SupportingDocID=c.SupportingDocumentId and
ct.RequestId=f.RequestId and
b.CertificateRequestId=f.RequestId and
 b.Status like @Status and b.CustomerID like @requestID and ct.CertificateId like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )




 union


 select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
b.CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,
 f.Consignor,

f.Consignee,

f.InvoiceNo,
ct.CertificateId


from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e,tblCertifcateRequestHeader f,tblCertificate ct

where b.SupportingDocID=c.SupportingDocumentId and
b.CertificateRequestId=f.RequestId and
ct.RequestId=f.RequestId and
 b.Status like @Status and b.CustomerID like @requestID and ct.CertificateId like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )
 order by  RequestDate Desc

END

GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportingDocumentDownDate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetSupportingDocumentDownDate](@Status varchar(20),@requestID varchar(20),@reqstID varchar(20),@StartDate  varchar(10),@EndDate varchar(10))
AS
BEGIN
select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
'SD Request'CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,
'Not Given' as Consignor,

'Not Given' as Consignee,
'Not Given' as InvoiceNo,
'SD Request' as CertificateId



from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e

where b.SupportingDocID=c.SupportingDocumentId and
b.CertificateRequestId is null and 
 b.Status like @Status and b.CustomerID like @requestID and b.RequestID like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )

	and 
convert(varchar,B.RequestDate,112)>=@StartDate
and convert(varchar,B.RequestDate,112)<=@EndDate



 union


 select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
b.CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,
 f.Consignor,
 f.InvoiceNo,
f.Consignee,
ct.CertificateId




from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e,tblCertifcateRequestHeader f,tblCertificate ct

where b.SupportingDocID=c.SupportingDocumentId and
ct.RequestId=f.RequestId and
b.CertificateRequestId=f.RequestId and
 b.Status like @Status and b.CustomerID like @requestID and ct.CertificateId like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )
	and 
convert(varchar,B.RequestDate,112)>=@StartDate
and convert(varchar,B.RequestDate,112)<=@EndDate


 union


 select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
b.CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,

'Not Given' as Consignor,

'Not Given' as Consignee,
f.InvoiceNo,
ct.CertificateId


from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e,tblUploadBasedCertificateRequest f,tblCertificate ct

where b.SupportingDocID=c.SupportingDocumentId and
ct.RequestId=f.RequestId and
b.CertificateRequestId=f.RequestId and
 b.Status like @Status and b.CustomerID like @requestID and ct.CertificateId like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )
	and 
convert(varchar,B.RequestDate,112)>=@StartDate
and convert(varchar,B.RequestDate,112)<=@EndDate

 order by  RequestDate Desc

END

GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportingDocumentN]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetSupportingDocumentN](@IsActive varchar(20))
AS
BEGIN
select
b.TemplateName,
c.SupportingDocumentName,
b.TemplateId,
c.SupportingDocumentId,
a.IsMandatory,
a.TemplateSupportingDocument

from tblTemplateSupportingDocument a,tblTemplateHeader b, tblSupportingDocuments c
where b.TemplateId=a.TemplateId and c.SupportingDocumentId=a.SupportingDocumentId and a.IsActive like @IsActive
order by b.TemplateName

END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportingDocUsingCertificateId]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetSupportingDocUsingCertificateId]
	(@CertificateId varchar(20),@invoiceSupDocID varchar(20))
AS
BEGIN
	SELECT A.RequestID,A.CustomerID,'I' AS DocType
	 FROM tblSupportingDocApproveRequest A,tblCertificate B
	WHERE 
	B.CertificateId=@CertificateId
	AND
	A.CertificateRequestId=B.RequestId
	AND
	SupportingDocID=@invoiceSupDocID

	UNION
	SELECT A.RequestID,A.CustomerID,'O' AS DocType
	 FROM tblSupportingDocApproveRequest A,tblCertificate B
	WHERE 
	B.CertificateId=@CertificateId
	AND
	A.CertificateRequestId=B.RequestId
	AND
	SupportingDocID!=@invoiceSupDocID

END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetSuppotingDocumentPeriodicDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetSuppotingDocumentPeriodicDetail] 
	(@customerId varchar(20),@status varchar(1),@startDate varchar(8),@endDate varchar(8)
	,@InvoiceRateId varchar(20),@OtherRateId varchar(20),@supdocInvoiceRateId varchar(20),
	@supdocOtherRateId varchar(20),@AtachSheetId varchar(20))
AS
BEGIN
	SELECT
	A.RequestID,A.UploadDocName,A.SupportingDocID,B.Rates,B.RatesId
	FROM
	tblSupportingDocApproveRequest A,tblCustomerApplicableRates B
	WHERE
	A.CustomerID=@customerId
	AND
	A.SupportingDocID=@supdocInvoiceRateId
	AND 
	A.SupportingDocID!=@AtachSheetId
	AND
	B.RatesId=@InvoiceRateId
	AND
	A.RequestID NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.RequestID)

	AND
	A.RequestID
	not in
	(SELECT SupportingDocName FROM tblInvoiceRate WHERE SupportingDocName=A.RequestID)
	AND
	B.CustomerId=A.CustomerID
	AND
	Status=@status
	AND
	convert(varchar,ApprovedDate,112)>=@startDate
	AND
	convert(varchar,ApprovedDate,112)<=@endDate
	UNION
	SELECT
	A.RequestID,A.UploadDocName,A.SupportingDocID,B.Rates,B.RatesId
	FROM
	tblSupportingDocApproveRequest A,tblCustomerApplicableRates B
	WHERE
	A.CustomerID=@customerId
	AND
	A.SupportingDocID!=@supdocInvoiceRateId
	AND
	A.SupportingDocID!=@AtachSheetId
	AND
	B.RatesId=@OtherRateId
	AND
	
	A.RequestID NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.RequestID)
	AND
	A.RequestID
	not in
	(SELECT SupportingDocName FROM tblInvoiceRate WHERE SupportingDocName=A.RequestID)
	AND
	B.CustomerId=A.CustomerID
	AND
	Status=@status
	AND
	convert(varchar,ApprovedDate,112)>=@startDate
	AND
	convert(varchar,ApprovedDate,112)<=@endDate
	

		UNION
	SELECT
	A.ReferenceNo As RequestID,Null AS UploadDocName,@supdocInvoiceRateId As SupportingDocID,B.Rates,B.RatesId
	FROM
	tblManualCertificate A,tblCustomerApplicableRates B
	WHERE
	A.CustomerID=@customerId
	AND
	A.ItemDescription like 'I'
	AND
	B.RatesId=@InvoiceRateId
	AND
	
	A.ReferenceNo NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.ReferenceNo)
	AND
	A.ReferenceNo
	not in
	(SELECT SupportingDocName FROM tblInvoiceRate WHERE SupportingDocName=A.ReferenceNo)
	AND
	B.CustomerId=A.CustomerID
	AND
	Status=@status
	AND
	convert(varchar,A.CreatedDate,112)>=@startDate
	AND
	convert(varchar,A.CreatedDate,112)<=@endDate


		UNION
	SELECT
	A.ReferenceNo As RequestID,Null AS UploadDocName,Null As SupportingDocID,B.Rates,B.RatesId
	FROM
	tblManualCertificate A,tblCustomerApplicableRates B
	WHERE
	A.CustomerID=@customerId
	AND
	A.ItemDescription like 'O'
	AND
	B.RatesId=@OtherRateId
	AND
	
	A.ReferenceNo NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=A.ReferenceNo)
	AND
	A.ReferenceNo
	not in
	(SELECT SupportingDocName FROM tblInvoiceRate WHERE SupportingDocName=A.ReferenceNo)
	AND
	B.CustomerId=A.CustomerID
	AND
	Status=@status
	AND
	convert(varchar,A.CreatedDate,112)>=@startDate
	AND
	convert(varchar,A.CreatedDate,112)<=@endDate
	

END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupprtngDocName]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DCISgetSupprtngDocName](@SupportingDocumentName varchar(150))
as
begin
select SupportingDocumentName from tblSupportingDocuments
where SupportingDocumentName like @SupportingDocumentName
end










GO
/****** Object:  StoredProcedure [dbo].[DCISgetTax]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: Shirandi Ekanayake

-- Create date: 2016/05/25

-- Description: To Retrieve User Group Information

-- =============================================

CREATE 

PROCEDURE[dbo].[DCISgetTax]
(

@TaxCode  varchar (20),@IsActive varchar(2))
AS

BEGIN

select * from tblTax
where

TaxCode like @TaxCode 
and IsActive like @IsActive




　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetTaxCode]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DCISgetTaxCode](@TaxCode varchar(20))
as
begin
select TaxCode from tblTax
where TaxCode  like @TaxCode 
end











GO
/****** Object:  StoredProcedure [dbo].[DCISgetTaxDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetTaxDetails] 
	(@status varchar(1),@IsVat varchar(4))

	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if(@IsVat='0')
	SELECT * FROM tblTax WHERE IsActive=@status ORDER BY TaxPriority
	else
	SELECT * FROM tblTax
	 WHERE
	 IsActive=@status
	 AND
	 TaxCode!=@IsVat
	 ORDER BY TaxPriority
END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetTaxPriorityList]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetTaxPriorityList](@PriorityNo varchar(20))
AS
BEGIN
	Select * from tblTaxPriorityList where convert(varchar,PriorityNo) like @PriorityNo 
END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetTempalteID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetTempalteID](@TemplateId varchar(20))
AS
BEGIN
	Select * from tblTemplateHeader where TemplateId like @TemplateId 
END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetTemplateDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetTemplateDetails] 
	(@Active varchar(1))
	
AS
BEGIN
	SELECT
		TemplateId,TemplateName,ImgUrl,Description
	FROM
	tblTemplateHeader
	WHERE
	IsActive=@Active
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetTemplateHeader]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE

PROCEDURE[dbo].[DCISgetTemplateHeader]
(

@TemplateId varchar (20),@IsActive varchar (2))
AS

BEGIN

select * from tblTemplateHeader
where
TemplateId like @TemplateId and IsActive like @IsActive

　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetTemplateIDforSupDoc]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE

PROCEDURE[dbo].[DCISgetTemplateIDforSupDoc]
(

@TemplateId varchar (20),@TemplateName varchar (100) )
AS

BEGIN

select TemplateID,TemplateName from tblTemplateHeader
where

TemplateID like @TemplateId and TemplateName like @TemplateName



　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetTemplateNameforSupDoc]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE

PROCEDURE[dbo].[DCISgetTemplateNameforSupDoc]
(

@TemplateName varchar (20))
AS

BEGIN

select TemplateName,TemplateId from tblTemplateHeader
where

TemplateName like @TemplateName


　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetTemplateSupportingDocument]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE
PROCEDURE[dbo].[DCISgetTemplateSupportingDocument]
(

@TemplateId varchar (20),@IsActive varchar(2))
AS

BEGIN

select * from tblTemplateSupportingDocument
where

TemplateId like @TemplateId and IsActive like @IsActive

　

END










GO
/****** Object:  StoredProcedure [dbo].[DCISgetTemplateUnitCharge]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetTemplateUnitCharge](@IsActive varchar(20))
AS
BEGIN
select
a.*,
b.TemplateName


from tblCertificateUnitCharge a,tblTemplateHeader b
where b.TemplateId=a.TemplateId  

END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetUnitChage]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetUnitChage](@TemplateId varchar(20))
AS
BEGIN
	Select * from tblCertificateUnitCharge where TemplateId like @TemplateId
END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetUploadBasedCertReq]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetUploadBasedCertReq] 
	(@RequestId varchar(20),@Status varchar(3))
AS
BEGIN

SELECT
	A.CertificateId,A.CreatedBy,A.CreatedDate,A.CustomerId,A.Status,A.Remark,A.RequestId,C.CertificateName,C.CertificatePath
FROM  
	tblUploadBasedCertificateRequest A,
	tblCertificate C
	WHERE 
	 A.Status like @Status
	  and A.RequestId=C.RequestId
	  and A.Remark !='C'

union

SELECT
	B.CertificateId,B.CreatedBy,B.CreatedDate,B.CustomerId,B.Status,B.Remark,B.RequestId,C.CertificateName,C.CertificatePath
FROM  
	tblEmailBasedCertificateRequest B,
	tblCertificate C
	WHERE 
	 B.Status like @Status
	  and B.RequestId=C.RequestId
	  and B.Remark !='C'





	





END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetUploadBasedCertReqE]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISgetUploadBasedCertReqE] 
	(@RequestId varchar(20),@Status varchar(3))
AS
BEGIN

SELECT
	A.CertificateId,A.CreatedBy,A.CreatedDate,A.CustomerId,A.Status,A.Remark,A.RequestId,C.CertificateName,C.CertificatePath
FROM  
	tblUploadBasedCertificateRequest A,
	tblCertificate C
	WHERE 
	 A.Status like @Status
	  and A.RequestId=C.RequestId
	  and A.Remark ='C'

	  union

SELECT
	B.CertificateId,B.CreatedBy,B.CreatedDate,B.CustomerId,B.Status,B.Remark,B.RequestId,C.CertificateName,C.CertificatePath
FROM  
	tblEmailBasedCertificateRequest B,
	tblCertificate C
	WHERE 
	 B.Status like @Status
	  and B.RequestId=C.RequestId
	  and B.Remark ='C'





	





END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetUploadBCRequestSupportingDOC]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[DCISgetUploadBCRequestSupportingDOC](@RequestID varchar(20))
as begin

Select
RequestRefNo, 
A.SupportingDocumentId,
B.SupportingDocumentName, 
Remarks, 
UploadedDate, 
UploadedBy, 
UploadedPath, 
UploadSeqNo,
DocumentName,
RequestDate

from tblSupportingDOCUpload A, tblSupportingDocuments B,tblUploadBasedCertificateRequest C
where a.SupportingDocumentId = B.SupportingDocumentId
and a.RequestRefNo = c.RequestId
and C.RequestId = @RequestID

End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetUploadRegistrationLetter]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetUploadRegistrationLetter]
	(@CustomerId varchar(20))
AS
BEGIN
	SELECT A.CreatedDate,A.RegistrationLetterPath,A.RequestLetterPath,B.CustomerName FROM tblCustomerRegistartionFiles A,tblCustomer B WHERE 
	A.CustomerId=@CustomerId
	and
	A.CustomerId=B.CustomerId
END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetuploadReqCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetuploadReqCount]
AS
BEGIN

select
	COUNT (Status)
	from
	 tblUploadBasedCertificateRequest
	 where status='P' 


END



GO
/****** Object:  StoredProcedure [dbo].[DCISgetUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE

PROCEDURE[dbo].[DCISgetUser]
(

@UserId varchar (20),@IsActive varchar(1))
AS

BEGIN

select * from tblUser
where

UserID like @UserID
and

IsActive like @IsActive
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserCertificateRequestsBy]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DCISgetUserCertificateRequestsBy](@UserId varchar(20),@Status varchar(2))
as begin


select 

RequestId, 
TemplateId,
A.CustomerId, 
RequestDate, 
ModifiedDate,   
A.Status,
 Consignor, 
 Consignee, 
 InvoiceNo, 
 InvoiceDate, 
 B.CountryName, 
 LoadingPort, 
 PortOfDischarge, 
 Vessel, 
 PlaceOfDelivery, 
 TotalInvoiceValue, 
 TotalQuantity

 from tblCertifcateRequestHeader A,tblCountry B,tblCustomer C,tblUser D
 where A.CustomerId = c.CustomerId
 and A.CountryCode = B.CountryCode
 and A.CustomerId =D.CustomerId
 and D.UserID=@UserId
 and A.Status = @Status


 End





GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserEdit]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE

PROCEDURE[dbo].[DCISgetUserEdit]
(

@UserId varchar (20),@IsActive varchar(1),@UserGrp varchar(20),@CustomerName varchar(50))
AS

BEGIN
if(@CustomerName='%')
select a.*,b.CustomerName from tblUser a,tblCustomer b
where
a.CustomerId=b.CustomerId and 
a.UserID like @UserID
and
a.UserGroupID like @UserGrp
and
a.IsActive like @IsActive and
b.CustomerId like @CustomerName

union

select a.*,  'none' as CustomerName from tblUser a
where
 a.UserGroupID='Admin' and
a.UserID like @UserID
and
a.UserGroupID like @UserGrp
and
a.IsActive like @IsActive

union

select a.*,  'none' as CustomerName from tblUser a
where
 a.UserGroupID='SAdmin' and
a.UserID like @UserID
and
a.UserGroupID like @UserGrp
and
a.IsActive like @IsActive


union

select a.*,  'none' as CustomerName from tblUser a
where
 a.UserGroupID='FAdmin' and
a.UserID like @UserID
and
a.UserGroupID like @UserGrp
and
a.IsActive like @IsActive
order by A.CustomerId

else
select a.*,b.CustomerName from tblUser a,tblCustomer b
where
a.CustomerId=b.CustomerId and 
a.UserID like @UserID
and
a.UserGroupID like @UserGrp
and
a.IsActive like @IsActive and
b.CustomerId like @CustomerName

END







GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserEditCAdmin]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create

PROCEDURE[dbo].[DCISgetUserEditCAdmin]
(

@UserId varchar (20),@IsActive varchar(1),@CusID varchar(20))
AS

BEGIN

select * from tblUser
where

UserID like @UserID
and
CustomerId like @CusID
and
IsActive like @IsActive
　

END








GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserGroup]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: Shirandi Ekanayake

-- Create date: 2016/05/25

-- Description: To Retrieve User Group Information

-- =============================================

CREATE

PROCEDURE[dbo].[DCISgetUserGroup]
(

@GroupId varchar (20),@IsActive varchar(1))
AS

BEGIN

select * from tblUserGroup
where

GroupId like @GroupId
and

IsActive like @IsActive
　

END










GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserId]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetUserId](@userid varchar(20))
as
begin
select UserID from tblUser
where UserID like @userid
end











GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserIDfromCusID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create

PROCEDURE[dbo].[DCISgetUserIDfromCusID]
(

@IsActive varchar(1),@CustomerId varchar(20))
AS

BEGIN

select PersonName,UserID,CustomerId from tblUser
where


 CustomerId like @CustomerId
 and
IsActive like @IsActive

END









GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserIDfromReqID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DCISgetUserIDfromReqID]@reqid varchar(20)
as
begin
select CreatedBy from tblCertifcateRequestHeader
where RequestId like @reqid 

union

select CreatedBy from tblUploadBasedCertificateRequest
where RequestId like @reqid 
end










GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserlogin]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetUserlogin](@UserId varchar(20),@Password nvarchar(50))

as begin

select UserId,UserGroupID,IsActive,Password,PassowordExpiryDate,PersonName,CustomerId
from tblUser 
where UserId = @UserId
and Password = @Password


End








GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserPassword]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetUserPassword](@userid varchar(20),@Password varchar(200))
as
begin
select Password,UserID from tblUser
where UserID like @userid 
end











GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetUserRequest]
	(@Status varchar(20))
AS
BEGIN
	
	SELECT 
	 * FROM 
	tblUserRequest 
	WHERE 
	Status=@Status
	
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserRequestDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetUserRequestDetail]
	-- Add the parameters for the stored procedure here
	(@UserRequestID varchar(20))
AS
BEGIN
	
	SELECT * FROM tblUserRequest WHERE UserRequestId=@UserRequestID
END





GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserSignatureDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISgetUserSignatureDetails](@UserID varchar(20))
as Begin

SELECT
UserID, 
PFXpath, 
SignatureIMGPath
from tblUserSignature
where UserID = @UserID

End



GO
/****** Object:  StoredProcedure [dbo].[DCISgetUserTaxDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetUserTaxDetails]
	(@CustomerId varchar(20),@IsActive varchar(1))
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT
	 A.CustomerId, A.TaxCode, B.TaxName,B.TaxPercentage,A.TaxRegistrationNo
	 FROM
	 tblCustomerApplicableTax A,tblTax B
	 WHERE
	 A.TaxCode=B.TaxCode
	 AND
	 A.CustomerId=@CustomerId
	 AND
	 A.IsActive=@IsActive

END





GO
/****** Object:  StoredProcedure [dbo].[DCISModifCertifcateRequestHeader]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifCertifcateRequestHeader]
(
@CreatedBy varchar(20), 
 @RequestId varchar(20),
@Consignor varchar(100), 
@Consignee varchar(100), 
@InvoiceNo varchar(50), 
@InvoiceDate date,
 @CountryCode varchar(50), 
 @LoadingPort varchar(50), 
 @PortOfDischarge varchar(50), 
 @Vessel varchar(50), 
 @PlaceOfDelivery varchar(50), 
 @TotalInvoiceValue varchar(50), 
 @TotalQuantity varchar(20))
AS
BEGIN
update tblCertifcateRequestHeader
set
 ModifiedDate=getdate(), ModifiedBy=@CreatedBy, 
 Consignor=@Consignor, Consignee=@Consignee, InvoiceNo=@InvoiceNo, InvoiceDate=@InvoiceDate,
 CountryCode=@CountryCode, LoadingPort=@LoadingPort, PortOfDischarge=@PortOfDischarge, Vessel=@Vessel, PlaceOfDelivery=@PlaceOfDelivery, TotalInvoiceValue=@TotalInvoiceValue, TotalQuantity=@TotalQuantity

 where RequestId=@RequestId
END







GO
/****** Object:  StoredProcedure [dbo].[DCISModifycertificateconfig]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[DCISModifycertificateconfig]
(

@CustomerId varchar(20),@Xcord decimal(18, 2),@Ycord decimal(18, 2),@Height decimal(18, 2),@Width decimal(18, 2))
AS

BEGIN

update tblEmailCertificateConfig
set 

LLXcordinates=@Xcord,
LLYcordinates=@Ycord,
URXcordinates=@Height,
URYcordinates=@Width


where
CustomerId=@CustomerId
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyCertificateRequestDetailsM]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISModifyCertificateRequestDetailsM](@RequestId varchar(20), @GoodItem varchar(50), 
@ShippingMark varchar(50), @PackageType varchar(5), 
@SummaryDesc varchar(100), @Quantity varchar(20), @HSCode varchar(50),@seqno varchar(20))
AS
BEGIN
update tblCertificateRequestDetails
set
RequestId=@RequestId, GoodItem=@GoodItem, ShippingMark=@ShippingMark, PackageType=@PackageType, SummaryDesc=@SummaryDesc, Quantity=@Quantity, HSCode=@HSCode
where SeqNo=@seqno



END



GO
/****** Object:  StoredProcedure [dbo].[DCISModifyCertificateUnitCharge]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyCertificateUnitCharge]
(

@TemplateId varchar(20),@ModifiedBy varchar(150),@UnitChargeValue Decimal(18,8))
AS

BEGIN

update tblCertificateUnitCharge
set ModifiedBy= @ModifiedBy,ModifiedDate=getdate(),UnitChargeValue=@UnitChargeValue
where
TemplateId=@TemplateId ;
　

END





GO
/****** Object:  StoredProcedure [dbo].[DCISModifyCertificateValidity]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DCISModifyCertificateValidity]
(

@ReqId varchar(20), @IsValid varchar(50))
AS

BEGIN

update tblCertificate
set IsValid=@IsValid 



where
RequestId=@ReqId
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyConsignee]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[DCISModifyConsignee]
(

@Code varchar(20),@Description varchar(50))
AS

BEGIN

update tblConsignee
set 

Description=@Description



where
Code=@Code
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyConsigneeStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[DCISModifyConsigneeStatus]
(

@Code varchar(20),@Status varchar(2))
AS

BEGIN

update tblConsignee
set 

Status=@Status



where
Code=@Code
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyConsignor]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[DCISModifyConsignor]
(

@Code varchar(20),@Description varchar(50))
AS

BEGIN

update tblConsignor
set 

Description=@Description



where
Code=@Code
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyConsignorStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[DCISModifyConsignorStatus]
(

@Code varchar(20),@Status varchar(2))
AS

BEGIN

update tblConsignor
set 

Status=@Status



where
Code=@Code
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyCustomerDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISModifyCustomerDetails] 
	-- Add the parameters for the stored procedure here
	(@CustomerId varchar(20), @TemplateId varchar(20),
	 @CustomerName varchar(150), @Telephone varchar(20),
	  @Fax varchar(20), @Email varchar(50), @Address1 varchar(150), @Address2 varchar(150),
	   @Address3 varchar(150),@ContactPersonName varchar(150)
	 ,@ContactPersonDesignation varchar(50),@ContactPersonDirectPhoneNumber varchar(20),
	 @ContactPersonMobile varchar(20),@ContactPersonEmail varchar(50),
	 @Productdetails Text,@ExportSector varchar(10),@NCEMember varchar(10),@paidType varchar(10),@AdminName varchar(150))
AS
BEGIN
	
	UPDATE tblCustomer
	SET
	CustomerName=@CustomerName,
	Telephone=@Telephone,Fax=@Fax,Email=@Email,Address1=@Address1,Address2=@Address2,
	Address3=@Address3,ContactPersonName=@ContactPersonName,ContactPersonDesignation=@ContactPersonDesignation,
	ContactPersonDirectPhoneNumber=@ContactPersonDirectPhoneNumber,ContactPersonMobile=@ContactPersonMobile,
	ContactPersonEmail=@ContactPersonEmail,ProductDetails=@Productdetails,ExportSector=@ExportSector,
	NCEMember=@NCEMember,PaidType=@paidType
	WHERE
	CustomerId=@CustomerId
	
	UPDATE tblCustomerTemplate
	SET
	TemplateId=@TemplateId
	WHERE
	CustomerId=@CustomerId

	
END







GO
/****** Object:  StoredProcedure [dbo].[DCISModifyCustomerDetailWithCAdmin]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISModifyCustomerDetailWithCAdmin]
	-- Add the parameters for the stored procedure here
	(@CustomerId varchar(20), @TemplateId varchar(20),
	 @CustomerName varchar(150), @Telephone varchar(20),
	  @Fax varchar(20), @Email varchar(50), @Address1 varchar(150), @Address2 varchar(150),
	   @Address3 varchar(150),@ContactPersonName varchar(150)
	 ,@ContactPersonDesignation varchar(50),@ContactPersonDirectPhoneNumber varchar(20),
	 @ContactPersonMobile varchar(20),@ContactPersonEmail varchar(50),
	 @Productdetails Text,@ExportSector varchar(10),@NCEMember varchar(10),@paidType varchar(10),@AdminName varchar(150)
	 ,@NewUser varchar(20),@NewPassword varchar(200),@userId varchar(20),@createdBy varchar(20))
AS
BEGIN
	
	UPDATE tblCustomer
	SET
	CustomerName=@CustomerName,
	Telephone=@Telephone,Fax=@Fax,Email=@Email,Address1=@Address1,Address2=@Address2,
	Address3=@Address3,ContactPersonName=@ContactPersonName,ContactPersonDesignation=@ContactPersonDesignation,
	ContactPersonDirectPhoneNumber=@ContactPersonDirectPhoneNumber,ContactPersonMobile=@ContactPersonMobile,
	ContactPersonEmail=@ContactPersonEmail,ProductDetails=@Productdetails,ExportSector=@ExportSector,
	NCEMember=@NCEMember,PaidType=@paidType
	WHERE
	CustomerId=@CustomerId
	
	UPDATE tblCustomerTemplate
	SET
	TemplateId=@TemplateId
	WHERE
	CustomerId=@CustomerId

	UPDATE
	tblUser
	SET
	IsActive='N'
	WHERE
	CustomerId=@CustomerId
	AND
	UserGroupID='CADMIN'

	INSERT Into tblUser
(UserID, UserGroupID, PersonName, Password, CreatedBy, CreatedDate, UpdateDate, IsActive, PassowordExpiryDate,
 CustomerId, Designation)
 VALUES
 (@NewUser,'CADMIN',@ContactPersonName,@NewPassword,@createdBy,GETDATE(),null,'Y',GETDATE(),@CustomerId,@ContactPersonDesignation)

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyCustomerEmail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyCustomerEmail]
(

@UserID varchar(20), @email varchar(50))
AS

BEGIN

update tblCustomerEmail
set Email=@email



where
UserID=@UserID 
　

END








GO
/****** Object:  StoredProcedure [dbo].[DCISModifyCustomerExportSector]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISModifyCustomerExportSector] 
	(@CustomerSectorId int,@exportSector varchar(20),@CustomerId varchar(20),@status varchar(1))
AS
BEGIN
if(@exportSector='none')
DELETE FROM tblCustomerExportSector Where
	CustomerExportSectorId=@CustomerSectorId
--UPDATE
--	tblCustomerExportSector
--	SET
--	Status='N'
--	Where
	--CustomerExportSectorId=@CustomerSectorId
else if(@CustomerSectorId=0)
 
INSERT INTO tblCustomerExportSector(CustomerId,ExportSectorId,Status)
VALUES (@CustomerId,@exportSector,@status)
else
	UPDATE
	tblCustomerExportSector
	SET 
	ExportSectorId=@exportSector,
	Status=@status
	WHERE
	CustomerExportSectorId=@CustomerSectorId
END



GO
/****** Object:  StoredProcedure [dbo].[DCISModifyCustomerRate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISModifyCustomerRate]
	(@customerId varchar(20),@RateId varchar(10),@Rate decimal(18,6))
AS
BEGIN
	Update 
	tblCustomerApplicableRates
	SET
	Rates=@Rate
	WHERE
	RatesId=@RateId
	AND
	CustomerId=@customerId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISmodifyCustomerSVAT]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISmodifyCustomerSVAT]
	(@CustomerId varchar(20),@vat varchar(10))
AS
BEGIN
update tblCustomer
SET 
IsSVat=@vat
WHERE
CustomerId=@CustomerId
   
END



GO
/****** Object:  StoredProcedure [dbo].[DCISModifyCustomerTax]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISModifyCustomerTax]
	(@CustomerId varchar(20),@TaxCode varchar(20), @TaxRegistrationNo varchar(20),@IsActive varchar(1))
AS
BEGIN
if(@IsActive='N')
	DELETE FROM tblCustomerApplicableTax WHERE CustomerId=@CustomerId
	And
	TaxCode=@TaxCode
else
	UPDATE
	tblCustomerApplicableTax
	SET TaxRegistrationNo=@TaxRegistrationNo,IsActive=@IsActive
	WHERE
	CustomerId=@CustomerId
	And
	TaxCode=@TaxCode
END





GO
/****** Object:  StoredProcedure [dbo].[DCISModifyEmailbasedBasedCertRemarks]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISModifyEmailbasedBasedCertRemarks]
	(@RequestId varchar(20),@Remarks varchar(20))
AS
BEGIN
	UPDATE
	tblEmailBasedCertificateRequest
	SET Remark=@Remarks 
	WHERE
	RequestId like @RequestId;



	

	End




GO
/****** Object:  StoredProcedure [dbo].[DCISModifyExportSector]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DCISModifyExportSector]
(

@ExportSectorName varchar(20),@ExportSectorDescription varchar(150),@Status varchar(2))
AS

BEGIN

update tblExportSector
set Status= @Status,ExportSector=@ExportSectorDescription
where
ExportId=@ExportSectorName ;
　

END





GO
/****** Object:  StoredProcedure [dbo].[DCISModifyExportSectorStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DCISModifyExportSectorStatus]
(

@ExportSectorName varchar(20),@Status varchar(2))
AS

BEGIN

update tblExportSector
set Status= @Status
where
ExportId=@ExportSectorName ;
　

END





GO
/****** Object:  StoredProcedure [dbo].[DCISModifyManualData]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyManualData]
(

@RefferenceNo varchar(20),@Status varchar(2),@IssuedDate varchar(15),@ItemDescription varchar(30),@ExporterInvoiceNo varchar(30),@createdby varchar(30),@CustomerId varchar(20))
AS

BEGIN

update tblManualCertificate
set  Status= @Status,IssuedDate=@IssuedDate,ItemDescription=@ItemDescription,ExporterInvoiceNo=@ExporterInvoiceNo,CreatedDate=getdate(),CreatedBy=@createdby,CustomerId=@CustomerId 
where
ReferenceNo=@RefferenceNo
　

END







GO
/****** Object:  StoredProcedure [dbo].[DCISModifyMassageViewStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[DCISModifyMassageViewStatus]
	(@SeqId bigint,@status varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblContactFormDetails SET ViewStatus=@status WHERE seqNo=@SeqId
END



GO
/****** Object:  StoredProcedure [dbo].[DCISModifyNECContactPersonDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISModifyNECContactPersonDetail]
	(@name varchar(150),@phone varchar(40),@email varchar(100),@web varchar(150),@fax varchar(15))
AS
BEGIN
DELETE FROM tblOwnerDetails WHERE OwnerId='3'

Insert into tblOwnerDetails(OrganizationName, OwnerId, PostBox, Address1, Address2, Address3, VARRegNo, 
SVATregNo, TelephoneNo, FaxNo, Email, WebAddress, ImageUrls, Name)
Values('National Chamber of Exporters','3','NO','NO','NO','NO','NO','NO','NO',@fax,@email,@web,@phone,@name )
	

END


GO
/****** Object:  StoredProcedure [dbo].[DCISModifyOwnerDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyOwnerDetails]
(@OwnerCompany varchar(30),@OwnerId varchar(20),

@Name varchar(50),@Address1 varchar(20) ,@Address2 varchar(20),@Address3 varchar(20),@TelephoneNo varchar(20),@email varchar(30))
AS

BEGIN

update tblOwnerDetails
set  Name=@Name ,Address1=@Address1,Address2=@Address2,Address3=@Address3,TelephoneNo=@TelephoneNo,Email=@email,OrganizationName=@OwnerCompany
where
OwnerId=@OwnerId
　

END







GO
/****** Object:  StoredProcedure [dbo].[DCISModifyPackageTypes]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyPackageTypes]
(

@PackageType varchar(20),@PackageDescription varchar(150),@ModifiedBy varchar(20))
AS

BEGIN

update tblPackageType
set PackageDescription= @PackageDescription,ModifiedBy=@ModifiedBy
where
PackageType=@PackageType;
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyPackageTypeStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISModifyPackageTypeStatus](

@PackageType varchar(20),@IsActive varchar(1),@ModifiedBy varchar(20))
AS

BEGIN

update tblPackageType
set IsActive=@IsActive,
ModifiedBy = @ModifiedBy,
ModifiedDate=getdate()
where PackageType=@PackageType
　

　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyParameter]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISModifyParameter]
	(@ParameterCode varchar(20), @ParameterDescription varchar(150), @ParameterValue varchar(50))
AS
BEGIN
	UPDATE 
	tblParameter
	SET ParameterDescription=@ParameterDescription,ParameterValue=@ParameterValue
	WHERE
	ParameterCode=@ParameterCode
END





GO
/****** Object:  StoredProcedure [dbo].[DCISModifyRandomID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DCISModifyRandomID]
(

@RandomID varchar(20),@UserID nvarchar(20))
AS

BEGIN

update tblUser
set 
RandomID=@RandomID

where
UserID=@UserID 
　

END

GO
/****** Object:  StoredProcedure [dbo].[DCISModifyRejectReasons]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyRejectReasons]
(

@RejectCode varchar(20),@Category varchar(20),@ReasonName varchar(150),@ModifiedBy varchar(20))
AS

BEGIN

update tblRejectReasons
set ReasonName= @ReasonName,Category=@Category,ModifiedBy=@ModifiedBy,ModifiedDate=getdate()
where
RejectCode=@RejectCode;
　

END





GO
/****** Object:  StoredProcedure [dbo].[DCISModifyRejectReasonsStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISModifyRejectReasonsStatus](

@RejectCode varchar(20),@IsActive varchar(1),@ModifiedBy varchar(20))
AS

BEGIN

update tblRejectReasons
set IsActive=@IsActive,
ModifiedBy = @ModifiedBy,
ModifiedDate=getdate()
where RejectCode =@RejectCode 
　

　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifySignatureLevels]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifySignatureLevels]
(

@LevelId varchar(20),@UserId varchar(20),@TemplateID varchar(20))
AS

BEGIN

update tblSignatureLevels
set LevelId= @LevelId
where
UserId=@UserId and TemplateId=@TemplateID
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifySupDocconfig]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[DCISModifySupDocconfig]
(

@SDId varchar(20),@LLXcord decimal(18, 2),@LLYcord decimal(18, 2),@URX decimal(18, 2),@URY decimal(18, 2))
AS

BEGIN

update tblSupportingDocumentConfig
set 

LLXcordinates=@LLXcord,
LLYcordinates=@LLYcord,
URXcordinates=@URX,
URYcordinates=@URY


where
SupportingDocId=@SDId
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifySupDocDownload]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[DCISModifySupDocDownload]
(

@ReqID varchar(20),@IsDownloaded varchar(2))
AS

BEGIN

update tblSupportingDocApproveRequest
set 

IsDownloaded =@IsDownloaded



where
RequestID=@ReqID
　

END







GO
/****** Object:  StoredProcedure [dbo].[DCISModifySupportingDocuments]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifySupportingDocuments]
(

@SupportingDocumentId varchar(20),@SupportingDocumentName varchar(100),@ModifiedBy varchar(20))
AS

BEGIN

update tblSupportingDocuments
set SupportingDocumentName= @SupportingDocumentName,ModifiedBy=@ModifiedBy,ModifiedDate=getdate()
where
SupportingDocumentId=@SupportingDocumentId;
　

END





GO
/****** Object:  StoredProcedure [dbo].[DCISModifySupportingDocumentsStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISModifySupportingDocumentsStatus](

@SupportingDocumentId varchar(20),@IsActive varchar(1),@ModifiedBy varchar(20))
AS

BEGIN

update tblSupportingDocuments
set IsActive=@IsActive,
ModifiedBy = @ModifiedBy,
ModifiedDate=getdate()
where SupportingDocumentId=@SupportingDocumentId
　

　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyTax]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyTax]
(

@TaxCode varchar(50),@TaxName nvarchar(50),@TaxPercentage Decimal(18,2),@TaxPriority int)
AS

BEGIN

update tblTax
set 
TaxName=@TaxName,
TaxPercentage=@TaxPercentage,
TaxPriority=@TaxPriority


where
TaxCode=@TaxCode
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyTaxStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyTaxStatus]
(

@TaxCode varchar(50),@IsActive varchar(2),@ModifiedBy varchar(20))
AS

BEGIN

update tblTax
set TaxCode=@TaxCode,
IsActive=@IsActive,
ModifiedBy=@ModifiedBy --,


--ModifiedDate=getdate()
where
TaxCode=@TaxCode;
　

END










GO
/****** Object:  StoredProcedure [dbo].[DCISModifytblCertificatedownload]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifytblCertificatedownload]
(

@RequestId varchar(20),@IsDownloaded varchar(20))
AS

BEGIN

update tblCertificate
set IsDownloaded= @IsDownloaded
where
RequestId=@RequestId;
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyTemplateHeaderStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISModifyTemplateHeaderStatus](

@TemplateName varchar(150),@IsActive varchar(1),@ModifiedBy varchar(20))
AS

BEGIN

update tblTemplateHeader
set IsActive=@IsActive,
ModifiedBy = @ModifiedBy,
ModifiedDate=getdate()
where TemplateName =@TemplateName 
　

　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyTemplateSupportingDocument]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyTemplateSupportingDocument]
(

@TemplateId varchar(20),@SupportingDocumentId varchar(20),@ModifiedBy varchar(20),@IsMandatory varchar(10),@templateSupOD0c bigint)
AS

BEGIN

update tblTemplateSupportingDocument
set SupportingDocumentId= @SupportingDocumentId,ModifiedBy=@ModifiedBy,ModifiedDate=getdate(),IsMandatory=@IsMandatory ,TemplateId=@TemplateId
where
TemplateSupportingDocument=@templateSupOD0c;
　

END







GO
/****** Object:  StoredProcedure [dbo].[DCISModifyTemplateSupportingDocumentStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISModifyTemplateSupportingDocumentStatus](

@TemplateId varchar(20),@IsActive varchar(1),@ModifiedBy varchar(20),@SupportingDocumentId varchar(20))
AS

BEGIN

update tblTemplateSupportingDocument
set IsActive=@IsActive,
ModifiedBy = @ModifiedBy,
ModifiedDate=getdate()
where TemplateId =@TemplateId and SupportingDocumentId=@SupportingDocumentId
　

　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyUploadBasedCertRemarks]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISModifyUploadBasedCertRemarks]
	(@RequestId varchar(20),@Remarks varchar(20))
AS
BEGIN
	UPDATE
	tblUploadBasedCertificateRequest
	SET Remark=@Remarks 
	WHERE
	RequestId like @RequestId;
	End




GO
/****** Object:  StoredProcedure [dbo].[DCISModifyUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISModifyUser]
(

@UserID varchar(20), @UserGroupID varchar(20),@IsActive varchar(1),@PersonName nvarchar(50),@Password nvarchar(200),@email varchar(50))
AS

BEGIN

update tblUser
set IsActive=@IsActive,
UserGroupID=@UserGroupID,
PersonName=@PersonName,

UpdateDate=getdate(),
Email=@email,
Password=@Password


where
UserID=@UserID;
　

END










GO
/****** Object:  StoredProcedure [dbo].[DCISModifyUserC]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[DCISModifyUserC]
(

@UserID varchar(20), @UserGroupID varchar(20),@IsActive varchar(1),@PersonName nvarchar(50),@Password nvarchar(200),@email varchar(50))
AS

BEGIN

update tblUser
set IsActive=@IsActive,
Designation=@UserGroupID,
PersonName=@PersonName,

UpdateDate=getdate(),
Email=@email


where
UserID=@UserID;
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISModifyUserGroup]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================

-- Author: Shirandi Ekanayake

-- Create date: 2016/05/25

-- Description: To Retrieve User Group Information

-- =============================================

CREATE

PROCEDURE[dbo].[DCISModifyUserGroup]
(

@GroupId varchar(20),@GroupName varchar(50),@IsActive varchar(1),@ModifiedBy varchar(20))
AS

BEGIN

update tblUserGroup
set IsActive=@IsActive,
GroupName=@GroupName,
ModifiedBy=@ModifiedBy,
ModifiedDate=getdate()
where
GroupId=@GroupId;
　

END










GO
/****** Object:  StoredProcedure [dbo].[DCISModifyUserGroupStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: Shirandi Ekanayake

-- Create date: 2016/05/25

-- Description: To Activate and deactivate User Groups

-- =============================================

CREATE PROCEDURE[dbo].[DCISModifyUserGroupStatus]
(

@GroupId varchar(20),@IsActive varchar(1),@ModifiedBy varchar(20))
AS

BEGIN

update tblUserGroup
set IsActive=@IsActive,
ModifiedBy = @ModifiedBy,
ModifiedDate=getdate()
where GroupId=@GroupId
　

　

END










GO
/****** Object:  StoredProcedure [dbo].[DCISModifyUserUsingCustomer]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISModifyUserUsingCustomer]
	(@UserId varchar(20),@IsActive varchar(1),@PersonName nchar(50))
AS
BEGIN
	UPDATE
	tblUser
	SET
	PersonName=@PersonName,IsActive=@IsActive,UpdateDate=GETDATE()
	WHERE
	UserID=@UserId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISnotUploadedSupportingDocuments]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[DCISnotUploadedSupportingDocuments](@RequestID varchar(20))
AS BEGIN
select 
A.SupportingDocumentId,
B.SupportingDocumentName,
A.IsMandatory, 
null as Remarks, 
null as UploadedDate, 
null as UploadedBy, 
null as UploadedPath, 
null as UploadSeqNo,
null as DocumentName,
null as RequestDate,
null as SignatureRequired


from tblTemplateSupportingDocument A, tblSupportingDocuments B, tblCertifcateRequestHeader C
where A.SupportingDocumentId=B.SupportingDocumentId
and A.TemplateId=C.TemplateId
and A.SupportingDocumentId not in 
(select 
A.SupportingDocumentId
from tblSupportingDOCUpload A, tblSupportingDocuments B,tblCertifcateRequestHeader C
where a.SupportingDocumentId = B.SupportingDocumentId
and a.RequestRefNo = c.RequestId
and C.RequestId = @RequestID)
and RequestId = @RequestID
and A.IsActive like 'Y'

END


GO
/****** Object:  StoredProcedure [dbo].[DCISPendingCustomerCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISPendingCustomerCount]
AS
BEGIN

select
	COUNT (Status)
	from
	 tblCustomerRequest
	 where status='P' 


END



GO
/****** Object:  StoredProcedure [dbo].[DCISPendingSupDocCounts]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISPendingSupDocCounts]
AS
BEGIN

select
	COUNT (Status)
	from
	tblSupportingDocApproveRequest
	 where status='P' 


END


GO
/****** Object:  StoredProcedure [dbo].[DCISPendingUserCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISPendingUserCount]
AS
BEGIN

select
	COUNT (Status)
	from
	 tblUserRequest
	 where status='P' 


END



GO
/****** Object:  StoredProcedure [dbo].[DCISSENDAPPROVALEMAIL]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[DCISSENDAPPROVALEMAIL] (@CustomerID varchar(20), @RequestId varchar(20))
AS BEGIN

select CertificateId, H.RequestId,c.CustomerId,c.ContactPersonEmail,'W' as CertificateType
from tblCertificate a,tblCustomer c, tblCertifcateRequestHeader h
where a.RequestId = h.RequestId
and h.CustomerId = c.CustomerId
and a.IsSend like 'N'
and h.CustomerId like @CustomerID
and a.RequestId like @RequestId

union

select a.CertificateId, H.RequestId,c.CustomerId,c.ContactPersonEmail,'U' as CertificateType
from tblCertificate a,tblCustomer c, tblUploadBasedCertificateRequest h
where a.RequestId = h.RequestId
and h.CustomerId = c.CustomerId
and a.isSend like 'N'
and h.CustomerId like @CustomerID
and a.RequestId like @RequestId

END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetApprove]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetApprove]
	(@UserID varchar(20), @UserGroupID nvarchar(20), 
	@Password nvarchar(200), @CreatedBy nvarchar(50), @IsActive varchar(1),
	 @PassowordExpiryDate datetime,@CustomerId varchar(20), @TemplateId varchar(20),
	 @CustomerName varchar(150), @Telephone varchar(20), @IsSVat varchar(10),
	  @Fax varchar(20), @Email varchar(50), @Address1 varchar(150), @Address2 varchar(150),
	   @Address3 varchar(150), @Status varchar(1),@ContactPersonName varchar(150)
	 ,@ContactPersonDesignation varchar(50),@ContactPersonDirectPhoneNumber varchar(20),
	 @ContactPersonMobile varchar(20),@ContactPersonEmail varchar(50),
	 @Productdetails Text,@RequestNo varchar(20),@NCEMember varchar(10),@Admin nvarchar(50))
AS
BEGIN

	INSERT INTO tblCustomer (CustomerId,CustomerName,Telephone,IsSVat,Fax,Email,
	Address1,Address2,Address3,Status,CreatedDate,CreatedBy,ContactPersonName
	 ,ContactPersonDesignation,ContactPersonDirectPhoneNumber,
	 ContactPersonMobile,ContactPersonEmail,
	 ProductDetails,NCEMember)
	VALUES
	(@CustomerId, @CustomerName, @Telephone, @IsSVat, @Fax, @Email, 
	@Address1, @Address2, @Address3, @Status, GETDATE(), @CreatedBy,@ContactPersonName
	,@ContactPersonDesignation,@ContactPersonDirectPhoneNumber,@ContactPersonMobile,@ContactPersonEmail
	,@Productdetails,@NCEMember);

	INSERT INTO tblUser (UserID,UserGroupID,PersonName,Password,CreatedBy,
	CreatedDate,UpdateDate,IsActive,PassowordExpiryDate,CustomerId,Designation,Email)
	VALUES
	(@UserID, @UserGroupID,@Admin, @Password, @CreatedBy, GETDATE(),
 null, @IsActive, @PassowordExpiryDate,@CustomerId,@ContactPersonDesignation,@ContactPersonEmail);
	INSERT INTO tblCustomerTemplate(CustomerId,TemplateId,CreatedDate,CreatedBy)
	 VALUES
	 (@CustomerId, @TemplateId,GETDATE(),@CreatedBy)
	 UPDATE tblCustomerRequest 
	 SET
	 Status=@Status
	 WHERE
	 AdminUserId=@UserID

	 UPDATE 
	 tblCustomerExportSector
	 SET
	CustomerId=@CustomerId
	 WHERE
		RequestNo=@RequestNo
	UPDATE 
	tblCustomerApplicableTax
	SET
	CustomerId=@CustomerId
	WHERE
	RequestId=@RequestNo
	Update
	tblCustomerRegistartionFiles 
	SET
	CustomerId=@CustomerId
	WHERE
	RequestId=@RequestNo
	 
END







GO
/****** Object:  StoredProcedure [dbo].[DCISsetCertifcateRequestHeader]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISsetCertifcateRequestHeader]
(@RequestId varchar(20), 
 @TemplateId varchar(20), 
 @CustomerId varchar(20),
@CreatedBy varchar(20), 
@Status varchar(3), 
@Consignor varchar(500), 
@Consignee varchar(500), 
@InvoiceNo varchar(50), 
@InvoiceDate date,
 @CountryCode varchar(50), 
 @LoadingPort varchar(50), 
 @PortOfDischarge varchar(50), 
 @Vessel varchar(50), 
 @PlaceOfDelivery varchar(50), 
 @TotalInvoiceValue varchar(50),
 @TotalQuantity varchar(20),
 @OtherComments varchar(150),
 @OtherDetails varchar(250),
 @SealRequired varchar(5))
AS
BEGIN
Insert into [dbo].[tblCertifcateRequestHeader]
(RequestId, TemplateId, CustomerId, RequestDate, ModifiedDate, ModifiedBy, CreatedDate, CreatedBy, 
Status, Consignor, Consignee, InvoiceNo, InvoiceDate,
 CountryCode, LoadingPort, PortOfDischarge, Vessel, PlaceOfDelivery, TotalInvoiceValue, TotalQuantity,OtherComments,OtherDetails,SealRequired)
 values
 (@RequestId, @TemplateId, @CustomerId, getdate(), null, null, getdate(), @CreatedBy, 
@Status, @Consignor, @Consignee, @InvoiceNo, @InvoiceDate,
 @CountryCode, @LoadingPort, @PortOfDischarge, @Vessel, @PlaceOfDelivery, @TotalInvoiceValue, @TotalQuantity,@OtherComments,@OtherDetails,@SealRequired)
END







GO
/****** Object:  StoredProcedure [dbo].[DCISsetCertificateApproval]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISsetCertificateApproval]
(
@CertificateId varchar(20),
@RequestId varchar(20), 
@ExpiryDate datetime, 
@CreatedBy varchar(20) , 
@IsDownloaded varchar(2), 
@CertificatePath varchar(500), 
@CertificateName varchar(150), 
@IsValid varchar(5)
)
as begin

Insert Into tblCertificate
(CertificateId,RequestId, CreatedDate, ExpiryDate, CreatedBy, IsDownloaded, CertificatePath, CertificateName, IsValid,IsSend) Values
(@CertificateId,@RequestId, GETDATE(), @ExpiryDate, @CreatedBy, @IsDownloaded, @CertificatePath, @CertificateName, @IsValid,'N')

Update tblCertifcateRequestHeader
Set Status = 'A'
where RequestId = @RequestId

Update tblWebBasedCertificateRequest
Set Status = 'A'
where RequestId = @RequestId

End



GO
/****** Object:  StoredProcedure [dbo].[DCISsetCertificateConfig]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISsetCertificateConfig](@CustomerId varchar(20),@Xcord decimal(18, 2),@Ycord decimal(18, 2),@Height decimal(18, 2),@Width decimal(18, 2))
AS

BEGIN


insert into tblEmailCertificateConfig
(CustomerId,LLXcordinates,LLYcordinates,URXcordinates,URYcordinates)
values
(
@CustomerId,@Xcord,@Ycord,@Height,@Width)
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetCertificateReject]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISsetCertificateReject](@RequestID varchar(20), @RejectBy varchar(20),@ResonCode varchar(10))
as begin

Update tblCertifcateRequestHeader
Set Status = 'R',
ModifiedBy = @RejectBy,
ReasonCode = @ResonCode,
ModifiedDate = GETDATE()
where RequestId = @RequestId
and Status like 'G'

End


GO
/****** Object:  StoredProcedure [dbo].[DCISsetCertificateRequestDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISsetCertificateRequestDetails](@RequestId varchar(20), @GoodItem varchar(500), 
@ShippingMark varchar(500), @PackageType varchar(500), 
@SummaryDesc varchar(1000), @Quantity varchar(500), @HSCode varchar(500),@CreatedBy varchar(20))
AS
BEGIN
insert into tblCertificateRequestDetails
(RequestId, GoodItem, ShippingMark, PackageType, SummaryDesc, Quantity, HSCode, CreatedDate, CreatedBy)
values
(@RequestId, @GoodItem, @ShippingMark, @PackageType, @SummaryDesc, @Quantity, @HSCode, getdate(), @CreatedBy)


END




GO
/****** Object:  StoredProcedure [dbo].[DCISsetCertificateUnitCharge]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISsetCertificateUnitCharge](@TemplateId varchar(20),@ModifiedBy varchar(150),@UnitChargeValue Decimal(16,6),@CreatedBy varchar(150),@IsActive varchar(2))
AS

BEGIN


insert into tblCertificateUnitCharge
(TemplateId,ModifiedBy,UnitChargeValue,CreatedBy,CreatedDate,ModifiedDate  )
values
(
@TemplateId ,@ModifiedBy,@UnitChargeValue,@CreatedBy,getdate(),getdate() )
　

END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetConsignee]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE[dbo].[DCISsetConsignee](@Code varchar(20),@Description varchar(50),@Status varchar(2) )
AS

BEGIN


insert into tblConsignee
(Code,Description,Status)
values
(
@Code,@Description,@Status)
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetConsignor]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create PROCEDURE[dbo].[DCISsetConsignor](@Code varchar(20),@Description varchar(50),@Status varchar(2) )
AS

BEGIN


insert into tblConsignor
(Code,Description,Status)
values
(
@Code,@Description,@Status)
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetContactFromDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetContactFromDetail]
	(@Name varchar(100),@Email varchar(50),@phone varchar(20),@Details text)
	AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO tblContactFormDetails(Name, Email, Phone, Detail, ViewStatus, CreatedDate) VALUES
	(@Name,@Email,@phone,@Details,'N',GETDATE())
END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetCustomer]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetCustomer]
	(@customerId varchar(20),@customerName varchar(150),@Telephone varchar(20),@Fax varchar(20),
	 @Email varchar(50), @Address1 varchar(150), @Address2 varchar(150), @Address3 varchar(150),
	  @Status varchar(10), @CreatedBy varchar(20))
AS
BEGIN
	INSERT INTO
	tblCustomer
	VALUES
	(@customerId,@customerName,@Telephone,'0',@Fax,@Email,@Address1,@Address2,@Address3,@Status,GETDATE(),@CreatedBy)
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetCustomerApplicableTax]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetCustomerApplicableTax] 
	(@CustomerId varchar(20), @TaxCode varchar(20), @TaxRegistrationNo varchar(20),
	@CreatedBy varchar(20),@IsActive varchar(1),@RequestId varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO tblCustomerApplicableTax (CustomerId, TaxCode, TaxRegistrationNo,CreatedBy,CreatedDate,IsActive,RequestId)
	 values
	(@CustomerId,@TaxCode,@TaxRegistrationNo,@CreatedBy,GETDATE(),@IsActive,@RequestId) 
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetCustomerExportSector]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetCustomerExportSector] 
	(@requestNo varchar(20),@sector varchar(20),@status varchar(1))
AS
BEGIN
	INSERT tblCustomerExportSector(RequestNo,ExportSectorId,Status) VALUES (@requestNo,@sector,@status)

END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetCustomerIsVat]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetCustomerIsVat]
	(@CustomerId varchar(20),@IsVat varchar(10))
AS
BEGIN
	UPDATE tblCustomer SET IsSVat=@IsVat WHERE CustomerId=@CustomerId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetCustomerRate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetCustomerRate]
	(@CustomerId varchar(20), @RatesId varchar(10), @Rates decimal(18,6))
AS
BEGIN
	
   INSERT INTO
   tblCustomerApplicableRates(CustomerId, RatesId, Rates)
   Values(@CustomerId,@RatesId,@Rates)
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetCustomerRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nipun
-- Create date: 05/26/2016
-- Description:	Insert data to tblCustomerRequest table
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetCustomerRequest]
	(@RequestId varchar(20),@Name varchar(100),@Telephone varchar(20),@Email varchar(50),
	@Fax varchar(20),@Status varchar(1), @Address1 nvarchar(50), @Address2 nvarchar(50),
	 @Address3 nvarchar(50),@TemplateId varchar(20),@CreatedBy varchar(20),@SVat varchar(10), 
	 @AdminUserId varchar(20), @AdminPassword varchar(200),@ContactPersonName varchar(150)
	 ,@ContactPersonDesignation varchar(50),@ContactPersonDirectPhoneNumber varchar(20),
	 @ContactPersonMobile varchar(20),@ContactPersonEmail varchar(50),
	 @Productdetails Text,@ExportSector varchar(10),@NCEMember varchar(10),@AdminName varchar(150))
AS
BEGIN
	INSERT INTO [dbo].[tblCustomerRequest]
	(RequestId, Name, Telephone, Email, Fax, Address1,Address2,Address3, Status, 
	CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, SVat, 
	TemplateId, AdminUserId, AdminPassword,ContactPersonName,ContactPersonDesignation,
	ContactPersonDirectPhoneNumber,ContactPersonMobile,ContactPersonEmail,Productdetails,ExportSector,NCEMember,AdminName)
	VALUES
	(
	@RequestId, @Name, @Telephone, @Email, @Fax, @Address1,@Address2,@Address3, @Status, 
	GETDATE(), @CreatedBy, null, @CreatedBy , @SVat, 
	@TemplateId, @AdminUserId, @AdminPassword,@ContactPersonName,@ContactPersonDesignation
	,@ContactPersonDirectPhoneNumber,@ContactPersonMobile,@ContactPersonEmail,@Productdetails,@ExportSector
	,@NCEMember,@AdminName
	)
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetDeleteCERTREQDETAILS]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetDeleteCERTREQDETAILS](@SeqNo bigint)
AS BEGIN
DELETE FROM tblCertificateRequestDetails
WHERE SeqNo = @SeqNo
END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetDeleteSavedCertificates]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

 
 
 Create PROC [dbo].[DCISsetDeleteSavedCertificates](@ModifiedBy varchar(20),@RequestId varchar(20))
  as Begin

  UPDATE tblCertifcateRequestHeader
  SET Status = 'D',
  ModifiedDate = GETDATE(),
  ModifiedBy = @ModifiedBy
  WHERE RequestId = @RequestId
  and Status like 'P'

  End
GO
/****** Object:  StoredProcedure [dbo].[DCISsetDeleteSupportingDocUpload]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetDeleteSupportingDocUpload](@UploadSeqNo bigint)
AS BEGIN
Delete from tblSupportingDOCUpload 
WHERE UploadSeqNo = @UploadSeqNo
END


GO
/****** Object:  StoredProcedure [dbo].[DCISsetDocumentCancelation]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetDocumentCancelation]
(@DocID varchar(20),@CustomerId varchar(20),@Remark varchar(200),@CancelBy varchar(20),@DocType varchar(1))
AS
BEGIN
	DELETE FROM tblCancelledCertificate WHERE DocumentId=@DocID
	
	INSERT 
	INTO 
	tblCancelledcertificate
	(DocumentId, CustomerId, Remark, CancelBy, CancelDate, DocumentType)
	VALUES
	(@DocID,@CustomerId,@Remark,@CancelBy,GETDATE(),@DocType)

END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetECertificateApproval]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISsetECertificateApproval]
(
@CertificateId varchar(20),
@RequestId varchar(20), 
@ExpiryDate datetime, 
@CreatedBy varchar(20) , 
@IsDownloaded varchar(2), 
@CertificatePath varchar(500), 
@CertificateName varchar(150), 
@IsValid varchar(5)
)
as begin

Insert Into tblCertificate
(CertificateId,RequestId, CreatedDate, ExpiryDate, CreatedBy, IsDownloaded, CertificatePath, CertificateName, IsValid) Values
(@CertificateId,@RequestId, GETDATE(), @ExpiryDate, @CreatedBy, @IsDownloaded, @CertificatePath, @CertificateName, @IsValid)

Update tblEmailBasedCertificateRequest
Set Status = 'A',
CertificateId = @CertificateId,
IsSend = 'N'
where RequestId = @RequestId

End


GO
/****** Object:  StoredProcedure [dbo].[DCISsetECertificateReject]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISsetECertificateReject](@RequestID varchar(20), @RejectBy varchar(20),@ResonCode varchar(10))
as begin

Update tblEmailBasedCertificateRequest
Set Status = 'R',
ModifiedBy = @RejectBy,
ReasonCode = @ResonCode,
ModifiedDate = GETDATE()
where RequestId = @RequestId

End



GO
/****** Object:  StoredProcedure [dbo].[DCISsetEmail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetEmail]
	(@EmailBody varchar(200),@EmailAddress varchar(50))
AS
BEGIN
	INSERT
	INTO 
	tblWaitingEmail(EmailBody, EmailAddress)
	VALUES
	(@EmailBody, @EmailAddress)

END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetEmailCertificateRequests]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetEmailCertificateRequests]
(
@RequestId varchar(20), 
@MailID varchar(150), 
@Email varchar(150), 
@CustomerId varchar(20), 
@RecivedDate datetime, 
@NoOfAttachments int, 
@Status varchar(5), 
@EmailSubject varchar(50), 
@CreatedBy varchar(20) 
)
AS BEGIN
INSERT INTO tblEmailBasedCertificateRequest 
(RequestId, MailID, Email, CustomerId, RecivedDate, NoOfAttachments, Status, EmailSubject, CreatedDate, CreatedBy)
VALUES
(@RequestId, @MailID, @Email, @CustomerId, @RecivedDate, @NoOfAttachments, @Status,@EmailSubject, GETDATE(), @CreatedBy)

END


GO
/****** Object:  StoredProcedure [dbo].[DCISsetExportSector]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISsetExportSector]
	(@ExportSectorName varchar(20),@Status varchar(2),@ExportSectorDescription varchar(50))
AS
BEGIN
	INSERT
	INTO 
	tblExportSector(ExportId, Status,ExportSector)
	VALUES
	(@ExportSectorName,@Status, @ExportSectorDescription)

END






GO
/****** Object:  StoredProcedure [dbo].[DCISsetIgnoredEmails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetIgnoredEmails]
( 
@MailID varchar(150), 
@Email varchar(150), 
@CustomerId varchar(20), 
@RecivedDate datetime, 
@EmailSubject varchar(50),  
@CreatedBy varchar(20) 
)
AS BEGIN
INSERT INTO tblIgnoredEmailRequest 
VALUES (@MailID, @Email, @CustomerId, @RecivedDate, @EmailSubject, 
GETDATE(), @CreatedBy, NULL, NULL)
END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetInvoiceDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetInvoiceDetails]
	(@RequestNo varchar(20),@UnitCharge decimal(18,8), @CreatedBy varchar(20),@InvoiceNo varchar(20))
AS
BEGIN

	INSERT INTO tblInvoiceDetail
	( RequestNo, CreatedDate,ModifiedDate, UnitCharge, 
	CreatedBy,Modifiedby, InvoiceNo)
	 VALUES
	 (@RequestNo,GETDATE(),GETDATE(),@UnitCharge,@CreatedBy,@CreatedBy,@InvoiceNo) 
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetInvoiceheader]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  [dbo].[DCISsetInvoiceheader]
	(@InvoiceNo varchar(20), @CustomerId varchar(10),
	  @FromDate DateTime, @ToDate DateTime, @GrossTotal decimal(18,6), @InvoiceTotal decimal(18,6),
	   @IsTaxInvoice varchar(4), @CreatedBy varchar(50), @PrintTimes int)
AS
BEGIN
	
	
	INSERT INTO tblInvoiceHeader 
	(InvoiceNo, CustomerId, FromDate, ToDate, GrossTotal, InvoiceTotal,
	 IsTaxInvoice, CreatedDate, CreatedBy, PrintTime)
	VALUES (@InvoiceNo,@CustomerId,Convert(varchar(30),@FromDate,110),Convert(varchar(30),@ToDate,110),@GrossTotal,@InvoiceTotal,@IsTaxInvoice,GETDATE(),@CreatedBy,@PrintTimes)

END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetInvoicePrintReson]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetInvoicePrintReson]
	(@InvoiceNo varchar(20),@Count int,@Reson text,@createdby varchar(20))
AS
BEGIN
	INSERT INTO
	tblInvoicePrintCount(InvoiceNo, PrintCount, PrintDate, PrintReason, PrintedBy)
	VALUES
	(@InvoiceNo,@Count,GETDATE(),@Reson,@createdby)

END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetInvoiceRate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetInvoiceRate]
	(@CustomerId varchar(20),@InvoiceNo varchar(20),@SuportingDocName varchar(50),@RateId varchar(10),@RateValue decimal(18,2),@CreatedBy varchar(20))
AS
BEGIN
	INSERT INTO tblInvoiceRate(CustomerId,InvoiceNo, SupportingDocName, RateId, RateValue, CreatedBy, CreatedDate)
	VALUES(@CustomerId,@InvoiceNo,@SuportingDocName,@RateId,@RateValue,@CreatedBy,GETDATE())
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetInvoiceTax]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetInvoiceTax]
	(@InvoiceNo varchar(20),@TaxCode varchar(50),@Amount decimal(10,6),@CreatedBy varchar(50),@TaxPercentage decimal(18,2))
AS
BEGIN
	INSERT INTO 
	tblInvoiceTax
	Values
	(@InvoiceNo,@TaxCode,@Amount,@CreatedBy,GETDATE(),@TaxPercentage)
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetLetterFilePath]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetLetterFilePath] 
	@RegistrationFilePath varchar(200),@RerquestLetterPath varchar(200),@customerId varchar(20),@RequestId varchar(20),@ISIN varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
--	DELETE FROM tblCustomerRegistartionFiles WHERE CustomerId=@customerId
--	Insert Into tblCustomerRegistartionFiles (CustomerId, RegistrationLetterPath, RequestLetterPath,CreatedDate,RequestId)
--	Values (@customerId,@RegistrationFilePath,@RerquestLetterPath,GETDATE(),@RequestId)

IF (@RegistrationFilePath IS NOT NULL AND @ISIN='Y') 
   BEGIN
	UPDATE tblCustomerRegistartionFiles SET RegistrationLetterPath=@RegistrationFilePath WHERE CustomerId=@customerId

	END
IF (@RerquestLetterPath IS NOT NULL AND @ISIN='Y') 
	 BEGIN
			UPDATE tblCustomerRegistartionFiles SET RequestLetterPath=@RerquestLetterPath WHERE CustomerId=@customerId
	END
		
ELSE

	Insert Into tblCustomerRegistartionFiles (CustomerId, RegistrationLetterPath, RequestLetterPath,CreatedDate,RequestId)
	Values (@customerId,@RegistrationFilePath,@RerquestLetterPath,GETDATE(),@RequestId)
	
 
END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetManualData]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISsetManualData]
	(@RefferenceNo varchar(20),@IssuedDate varchar(20),@ExporterInvoiceNo varchar(50),@ItemDescription varchar(50),@Status varchar(10),@createdby varchar(10),@CustomerId varchar(20))
AS
BEGIN
	INSERT
	INTO 
	tblManualCertificate(ReferenceNo,IssuedDate,ExporterInvoiceNo,ItemDescription, Status,CreatedBy,CreatedDate,CustomerId)
	VALUES
	(@RefferenceNo,@IssuedDate, @ExporterInvoiceNo,@ItemDescription,@Status,@createdby,getdate(),@CustomerId)

END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetPackageType]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISsetPackageType](@PackageType varchar(20),@PackageDescription nvarchar(150),@CreatedBy varchar(20),@IsActive varchar(2))
AS

BEGIN


insert into tblPackageType
(PackageType,PackageDescription,CreatedBy,IsActive,CreatedDate)
values
(
@PackageType,@PackageDescription,@CreatedBy,@IsActive,getdate())
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetPaidType]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetPaidType]
	(@CustomerId varchar(20),@paidType varchar(10))
AS
BEGIN
	UPDATE 
	tblCustomer
	SET
	PaidType=@paidType
	WHERE
	CustomerId=@CustomerId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetParameter]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetParameter]
	(@ParameterCode varchar(20), @ParameterDescription varchar(150), @ParameterValue varchar(50))
AS
BEGIN
	INSERT INTO 
	tblParameter (ParameterCode,ParameterDescription,ParameterValue)
	VALUES
	(@ParameterCode,@ParameterDescription,@ParameterValue)

END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetReffrennceRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[DCISsetReffrennceRequest](@Consignee varchar(250),@CustomerID varchar(20),@RequestId varchar(20))
as Begin
Insert into tblCustomerRequestReffrence
(Consignee, CustomerId, RequestId) values 
(@Consignee,@CustomerID,@RequestId)
End
GO
/****** Object:  StoredProcedure [dbo].[DCISsetRejectReasons]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[DCISsetRejectReasons](@RejectCode varchar(20),@ReasonName nvarchar(150),@Category varchar(20),@IsActive varchar(2),@CreatedBy varchar(20))
AS

BEGIN


insert into tblRejectReasons
(RejectCode,ReasonName,Category,IsActive,CreatedBy,CreatedDate)
values
(
@RejectCode,@ReasonName,@Category,@IsActive,@CreatedBy,getdate())
　

END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetRejectUBCertificate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROC [dbo].[DCISsetRejectUBCertificate](@ModifiedBy varchar(20),@RequestId varchar(20),@RejectReasonCode varchar(20))
  as Begin

  UPDATE tblUploadBasedCertificateRequest
  SET Status = 'R',
  ModifiedDate = GETDATE(),
  ModifiedBy = @ModifiedBy,
  RejectReasonCode = @RejectReasonCode
  WHERE RequestId = @RequestId
  and Status like 'P'

  End


GO
/****** Object:  StoredProcedure [dbo].[DCISsetRejectUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetRejectUser] 
	(@RequestId varchar(20),@status varchar(1),@RejetReason varchar(20))
AS
BEGIN
	UPDATE 
	tblUserRequest
	SET Status=@status,RejectReason=@RejetReason
	WHERE
	UserRequestId=@RequestId

END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetRemovedCertificate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISsetRemovedCertificate]
	(@ReqID varchar(20),@CreatedBy varchar(20),@RemoveReasons varchar(100))
AS
BEGIN
	INSERt into [dbo].[tblRemovedCertificateReasons]
	(ReqID,RemoveReason,CreatedDate,CreatedBy)
	values
	(
	@ReqID, @RemoveReasons, GETDATE(), @CreatedBy
	
	)
END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetROWCertificateDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISsetROWCertificateDetails]
(@RequestId varchar(20), @GoodDetails varchar(500), 
@QuantityDetails varchar(300), @HSCodeDetails varchar(300), @CreatedBy varchar(20))
as begin

INSERT INTO tblRowCertificateRequestDetails (RequestId, GoodDetails, QuantityDetails, HSCodeDetails, CreatedDate, CreatedBy)
VALUES (@RequestId, @GoodDetails, @QuantityDetails, @HSCodeDetails, GETDATE(), @CreatedBy)

END
GO
/****** Object:  StoredProcedure [dbo].[DCISsetSignatorySignatureDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[DCISsetSignatorySignatureDetails]
(@UserID varchar(20),
@PFXpath nvarchar(250),
@SignatureIMGPath nvarchar(250),
@CreatedBy varchar(20)
)
as begin

Insert into tblUserSignature
Values (@UserID, @PFXpath, @SignatureIMGPath, @CreatedBy, GETDATE())

End




GO
/****** Object:  StoredProcedure [dbo].[DCISsetSignatureLevels]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISsetSignatureLevels](@UserId varchar(20),@CreatedBy varchar(20),@IsActive varchar(2), @LevelId nvarchar(150),@TemplateId nvarchar(150))
AS

BEGIN


insert into tblSignatureLevels
(UserId,LevelId,TemplateId,CreatedBy,CreatedDate,IsActive)
values
(
@UserId,@LevelId,@TemplateId,@CreatedBy,getdate(),@IsActive)
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetSupDocConfig]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISsetSupDocConfig](@SDId varchar(20),@LLX decimal(18, 2),@LLY decimal(18, 2),@URX decimal(18, 2),@URY decimal(18, 2))
AS

BEGIN


insert into tblSupportingDocumentConfig
(SupportingDocId,LLXcordinates,LLYcordinates,URXcordinates,URYcordinates)
values
(
@SDId ,@LLX,@LLY,@URX,@URY)
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetSupportingDocApprove]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetSupportingDocApprove](
@RequestID varchar(20), @SupportingDocID varchar(20), 
@CustomerID varchar(20), @RequestBy varchar(20), 
@Status varchar(5),@UploadPath varchar(250), @UploadDocName varchar(50))
as Begin
INSERT INTO tblSupportingDocApproveRequest
(RequestID, SupportingDocID, CustomerID, RequestDate, RequestBy, Status, ApprovedBy, ApprovedDate, UploadPath, UploadDocName, DownloadPath, DownloadDocName)
VALUES
(@RequestID, @SupportingDocID, @CustomerID, GETDATE(), @RequestBy, @Status, NULL, NULL, @UploadPath, @UploadDocName, NULL, NULL)

End






GO
/****** Object:  StoredProcedure [dbo].[DCISsetSupportingDocApproveFrmCRquest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetSupportingDocApproveFrmCRquest](
@RequestID varchar(20), @SupportingDocID varchar(20), 
@CustomerID varchar(20), @RequestBy varchar(20), 
@Status varchar(5),@UploadPath varchar(250), @UploadDocName varchar(50),
@ApprovedBy varchar(20), @RequestDate datetime,@DownloadPath varchar(250),@DocExpireDate date,
@CertificateRequestId varchar(20)
)
as Begin
INSERT INTO tblSupportingDocApproveRequest
(RequestID, SupportingDocID, CustomerID, RequestDate, RequestBy, Status, ApprovedBy, ApprovedDate, UploadPath, UploadDocName, DownloadPath, DownloadDocName,DocExpireDate,IsDownloaded,IsValid,CertificateRequestId)
VALUES
(@RequestID, @SupportingDocID, @CustomerID,@RequestDate, @RequestBy, @Status, @ApprovedBy, GETDATE(), @UploadPath, @UploadDocName, @DownloadPath, @UploadDocName,@DocExpireDate,'N','Y',@CertificateRequestId)

End


GO
/****** Object:  StoredProcedure [dbo].[DCISsetSupportingDocSignatureRequired]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[DCISsetSupportingDocSignatureRequired](@UploadSeqNo bigint, @SignatureRequired varchar(5), @Remarks varchar(150))
as begin

UPDATE tblSupportingDOCUpload 
SET SignatureRequired = @SignatureRequired,
Remarks = @Remarks
WHERE UploadSeqNo = @UploadSeqNo

End


GO
/****** Object:  StoredProcedure [dbo].[DCISsetSupportingDocuments]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISsetSupportingDocuments]
	-- Add the parameters for the stored procedure here
	(@SupportingDocumentId varchar(20), @SupportingDocumentName nvarchar(100), @CreatedBy nvarchar(50),@IsActive varchar(2)  )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO tblSupportingDocuments (SupportingDocumentId,SupportingDocumentName, CreatedBy, CreatedDate,IsActive) values
	(@SupportingDocumentId, @SupportingDocumentName, @CreatedBy, GETDATE(),@IsActive)
END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetSupportingDocUpload]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISsetSupportingDocUpload](@RequestRefNo varchar(20), @DocumentId varchar(20),

@Remarks varchar(150), @UploadedBy varchar(20),@UploadPath varchar(250),@DocumentName varchar(150),@SignatureRequired varchar(5))

AS

BEGIN

insert into tblSupportingDOCUpload

(RequestRefNo, SupportingDocumentId, Remarks,UploadedDate, UploadedBy,UploadedPath,DocumentName,SignatureRequired)

values

(@RequestRefNo, @DocumentId, @Remarks,GETDATE(), @UploadedBy,@UploadPath,@DocumentName,@SignatureRequired)

 

END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetTax]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISsetTax](@IsActive varchar(2),@TaxCode nvarchar(150),@TaxName nvarchar(150),@TaxPercentage Decimal(18,2),@TaxPriority int)
AS

BEGIN


insert into tblTax
(TaxCode,TaxName,TaxPercentage,TaxPriority,IsActive)
values
(
@TaxCode,@TaxName,@TaxPercentage,@TaxPriority,@IsActive)
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISsettblCustomerExportSector]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[DCISsettblCustomerExportSector] 
	(@CustomerId varchar(20),@ExportSector varchar(200))
AS
BEGIN
	UPDATE tblCustomer SET ExportSector=@ExportSector
	WHERE
	CustomerId=@CustomerId

END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetTemplate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetTemplate]
	-- Add the parameters for the stored procedure here
	(@RequestId varchar(20),@TemplateId varchar(20))
AS
BEGIN
	

    -- Insert statements for procedure here
	UPDATE tblCustomerRequest SET TemplateId=@TemplateId 
	WHERE RequestId=@RequestId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetTemplateHeader]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISsetTemplateHeader](@TemplateName varchar(20),@TemplateId varchar(20),@CreatedBy nvarchar(20),@IsActive nvarchar(2),@ModifiedBy nvarchar(20),@ImgUrl varchar(200),@Description varchar(200))
AS

BEGIN


insert into tblTemplateHeader
(TemplateName,TemplateId,CreatedBy,CreatedDate,ModifiedDate,IsActive,ModifiedBy,ImgUrl,Description)
values
(
@TemplateName,@TemplateId ,@CreatedBy,getdate(),getdate(),@IsActive,@ModifiedBy,@ImgUrl,@Description)
　

END









GO
/****** Object:  StoredProcedure [dbo].[DCISsetTemplateSupportingDocument]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISsetTemplateSupportingDocument](@SupportingDocumentId varchar(20),@TemplateId varchar(20),@CreatedBy nvarchar(20),@IsActive nvarchar(2),@IsMandatory varchar(10))
AS

BEGIN


insert into tblTemplateSupportingDocument
(SupportingDocumentId,TemplateId,CreatedBy,IsActive,CreatedDate,IsMandatory)
values
(
@SupportingDocumentId,@TemplateId ,@CreatedBy,@IsActive,getdate(),@IsMandatory)
　

END


GO
/****** Object:  StoredProcedure [dbo].[DCISsetUpdateCertificateRequestHeadStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetUpdateCertificateRequestHeadStatus] (@RequestId varchar(20),@Status  varchar(5))
as begin

Update tblCertifcateRequestHeader
Set Status = @Status
where RequestId = @RequestId

End




GO
/****** Object:  StoredProcedure [dbo].[DCISsetUpdateEBCSend]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetUpdateEBCSend](@RequestID varchar(20),@IsSend varchar(2))
AS BEGIN
UPDATE tblEmailBasedCertificateRequest
SET IsSend = @IsSend
Where RequestId = @RequestID

End


GO
/****** Object:  StoredProcedure [dbo].[DCISsetUpdateRowCertificateDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetUpdateRowCertificateDetails](@SeqNo bigint,@RequestId varchar(20), @GoodDetails varchar(250), 
@QuantityDetails varchar(150), @HSCodeDetails varchar(150), @CreatedBy varchar(20))
as begin
UPDATE tblRowCertificateRequestDetails
SET GoodDetails = @GoodDetails,
QuantityDetails = @QuantityDetails,
HSCodeDetails = @HSCodeDetails
WHERE SeqNo = @SeqNo
END



GO
/****** Object:  StoredProcedure [dbo].[DCISsetUpdateSDApproveReq]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetUpdateSDApproveReq](@RequestID varchar(20),@ApprovedBy varchar(20),@DownloadPath varchar(250),@DownloadDocName varchar(50),@DocExpireDate date)
as Begin
UPDATE tblSupportingDocApproveRequest
SET Status = 'A',
ApprovedBy = @ApprovedBy,
ApprovedDate = GETDATE(),
DownloadPath = @DownloadPath,
DownloadDocName = @DownloadDocName,
DocExpireDate = @DocExpireDate,
IsDownloaded = 'N',
IsValid = 'Y'
WHERE RequestID = @RequestID
End



GO
/****** Object:  StoredProcedure [dbo].[DCISsetUpdateSDRejectReq]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetUpdateSDRejectReq](@RequestID varchar(20),@ApprovedBy varchar(20),@RejectReasonCode varchar(20))
as Begin
UPDATE tblSupportingDocApproveRequest
SET Status = 'R',
ApprovedBy = @ApprovedBy,
ApprovedDate = GETDATE(),
RejectReasonCode = @RejectReasonCode
where RequestID = @RequestID
End 



GO
/****** Object:  StoredProcedure [dbo].[DCISsetUpdateSupportingDoc]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCISsetUpdateSupportingDoc](    
@UploadedBy varchar(50), 
@UploadedPath varchar(250), 
@DocumentName varchar(150),
@UploadSeqNo bigint

) 
as begin
Update tblSupportingDOCUpload
Set 
Remarks = 'Edited', 
UploadedDate=GETDATE(), 
DocumentName = @DocumentName,
UploadedBy= @UploadedBy, 
UploadedPath = @UploadedPath
Where UploadSeqNo = @UploadSeqNo

End






GO
/****** Object:  StoredProcedure [dbo].[DCISsetUpdateSupportingDocUpload]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetUpdateSupportingDocUpload](@UploadSeqNo bigint, @UploadedPath varchar(250),@DocumentName varchar(150))
as Begin
UPDATE tblSupportingDOCUpload 
SET UploadedPath = @UploadedPath,
DocumentName = @DocumentName,
Remarks = 'CERTIFIED'
WHERE UploadSeqNo = @UploadSeqNo
End



GO
/****** Object:  StoredProcedure [dbo].[DCISsetUpdateTblParameter]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetUpdateTblParameter](@ParameterCode varchar(20),@ParameterValue varchar(200))
as
Begin
UPDATE tblParameter
SET ParameterValue = @ParameterValue
WHERE ParameterCode = @ParameterCode
End



GO
/****** Object:  StoredProcedure [dbo].[DCISsetUpdateUploadBCertifcateRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetUpdateUploadBCertifcateRequest](@ModifiedBy varchar(20),@CertificateId varchar(20),@RequestId varchar(20))
as Begin

  UPDATE tblUploadBasedCertificateRequest
  SET Status = 'A',
  ModifiedDate = GETDATE(),
  ModifiedBy = @ModifiedBy,
  CertificateId  = @CertificateId 
  WHERE RequestId = @RequestId

  End



GO
/****** Object:  StoredProcedure [dbo].[DCISsetUploadBasedCRequests]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISsetUploadBasedCRequests]
(
@RequestId varchar(20), @CustomerId varchar(20), @Status varchar(5), @CreatedBy varchar(20),@UploadPath varchar(250),@InvoiceNo varchar(50),@SealRequired varchar(5)
)
as Begin
INSERT INTO tblUploadBasedCertificateRequest
(RequestId, CustomerId, RequestDate,InvoiceNo, Status, CreatedDate, CreatedBy, ModifiedDate, ModifiedBy, CertificatePath, UploadPath, RejectReasonCode, CertificateId,SealRequired)VALUES 
(@RequestId, @CustomerId, GETDATE(),@InvoiceNo, @Status, GETDATE(), @CreatedBy, null, null,null,@UploadPath,null,null,@SealRequired)
End



GO
/****** Object:  StoredProcedure [dbo].[DCISsetUser]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetUser]
	-- Add the parameters for the stored procedure here
	(@UserID varchar(20), @UserGroupID nvarchar(50), @PersonName nvarchar(200), @Password nvarchar(200), @CreatedBy nvarchar(50), @CreatedDate Date, @IsActive varchar(1), @PassowordExpiryDate Date)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO tblUser (UserID, UserGroupID, PersonName, Password, CreatedBy, CreatedDate,UpdateDate, IsActive, PassowordExpiryDate) values
	(@UserID, @UserGroupID, @PersonName, @Password, @CreatedBy, GETDATE(),GETDATE(), @IsActive, @PassowordExpiryDate)
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetUserAdd]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISsetUserAdd]
	(@UserID varchar(20),@UserGroupID varchar(20),@PersonName varchar(20),@Password varchar(200),@CreatedBy varchar(20),@IsActive varchar(10),@PassowordExpiryDate varchar(10))
AS
BEGIN
	INSERt into [dbo].[tblUser]
	(UserID, UserGroupID,PersonName, Password, CreatedBy,IsActive,CreatedDate,UpdateDate,PassowordExpiryDate)
	values
	(
	@UserID, @UserGroupID, @PersonName, @Password, @CreatedBy, @IsActive,GETDATE(),GETDATE(),@PassowordExpiryDate
	
	)
END










GO
/****** Object:  StoredProcedure [dbo].[DCISsetUserAddN]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISsetUserAddN]
	(@UserID varchar(20),@UserGroupID varchar(20),@PersonName varchar(20),@Password varchar(200),@CreatedBy varchar(20),@IsActive varchar(10),@PassowordExpiryDate DateTime,@CustomerId varchar(20),@Designation varchar(20),@Email varchar(50) )
AS
BEGIN
	INSERt into [dbo].[tblUser]
	(UserID, UserGroupID,PersonName, Password, CreatedBy,IsActive,CreatedDate,UpdateDate,PassowordExpiryDate,CustomerId,Designation,Email)
	values
	(
	@UserID, @UserGroupID, @PersonName, @Password, @CreatedBy, @IsActive,GETDATE(),GETDATE(),@PassowordExpiryDate,@CustomerId,@Designation,@Email
	
	)
END

GO
/****** Object:  StoredProcedure [dbo].[DCISsetUserApproval]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISsetUserApproval]
	(@UserId varchar(20),@UserGroupID nvarchar(20),@PersonName varchar(20),
	@Password nvarchar(200),@CreatedBy nvarchar(50),@IsActive varchar(1),
	@PassowordExpiryDate dateTime,@UserRequestId varchar(20),@Status varchar(1),@CustomerID varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	INSERT INTO
	 tblUser
	 (UserID,UserGroupID,PersonName,Password,CreatedBy,CreatedDate,UpdateDate,IsActive,PassowordExpiryDate,CustomerId)
	 VALUES 
	  (@UserId,@UserGroupID,@PersonName,@Password,@CreatedBy,GETDATE(),GETDATE(),
	  @IsActive,@PassowordExpiryDate,@CustomerID);

	  UPDATE tblUserRequest SET Status=@Status,
	  ApprovedBy=@CreatedBy
	   WHERE UserRequestId=@UserRequestId
END





GO
/****** Object:  StoredProcedure [dbo].[DCISsetUserEmailC]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DCISsetUserEmailC]
	(@UserID varchar(20),@email varchar(50),@cudId varchar(50))
AS
BEGIN
	INSERt into [dbo].[tblCustomerEmail]
	(UserID,Email,CustomerId)
	values
	(
	@UserID,@email,@cudId
	
	)
END








GO
/****** Object:  StoredProcedure [dbo].[DCISsetUserGroup]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================

-- Author: Shirandi Ekanayake

-- Create date: 2016/05/25

-- Description: To Add New user Group

-- =============================================

CREATE PROCEDURE[dbo].[DCISsetUserGroup](@GroupId varchar(20),@GroupName nvarchar(50),@CreatedBy varchar(20),@IsActive varchar(1),@ModifiedBy varchar(20))
AS

BEGIN


insert into tblUserGroup
(GroupId,GroupName,CreatedDate,IsActive,CreatedBy,ModifiedBy,ModifiedDate)
values
(
@GroupId,@GroupName,getdate(),@IsActive,@CreatedBy,@ModifiedBy,getdate())
　

END










GO
/****** Object:  StoredProcedure [dbo].[DCISsetUserRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DCISsetUserRequest]
(
@UserRequestId varchar(20), 
@UserId varchar(20), 
@Status varchar(10), 
@CreatedBy varchar(20), 
@ApprovedBy nvarchar(20), 
@Password varchar(150),
@PersonName varchar(150), 
@UserGroupID varchar(20),
@ModifiedBy varchar(20),
@CustomerID varchar(20))

AS
BEGIN
insert into tblUserRequest
(UserRequestId,UserGroupID, UserId, Status, CreatedBy, CreatedDate, ApprovedBy, Password, ModifiedBy, ModifiedDate,PersonName,CustomerID)
values
(@UserRequestId,@UserGroupID, @UserId, @Status, @CreatedBy, GETDATE(), @ApprovedBy, @Password, @ModifiedBy, GETDATE(),@PersonName,@CustomerID)


END







GO
/****** Object:  StoredProcedure [dbo].[DCISsetUserSignatureDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISsetUserSignatureDetails]
(@UserID varchar(20), 
@PFXpath nvarchar(250), 
@SignatureIMGPath nvarchar(250), 
@CreatedBy varchar(20))

as begin

Insert Into
tblUserSignature(UserID, PFXpath, SignatureIMGPath, CreatedBy, CreatedDate)
Values (@UserID,@PFXpath,@SignatureIMGPath,@CreatedBy,GETDATE())

End






GO
/****** Object:  StoredProcedure [dbo].[DCISsetWebBasedCertificateCreation]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DCISsetWebBasedCertificateCreation](@RequestId varchar(20), @CertificatePath varchar(250) , @CertificateName varchar(50))
as Begin

INSERT INTO tblWebBasedCertificateRequest (RequestId, CertificatePath, CertificateName, CreatedDate, Status)
VALUES (@RequestId, @CertificatePath,@CertificateName,GETDATE(),'P')
END


GO
/****** Object:  StoredProcedure [dbo].[DCISsupDocCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DCISsupDocCount](@cusid  varchar(10))
AS
BEGIN
select
	COUNT (IsDownloaded)
	from
	 tblSupportingDocApproveRequest
	 where IsDownloaded !='Y' and Status='A' and CustomerID like @cusid and RequestID NOT in
	(SELECT D.DocumentId FROM tblCancelledcertificate D WHERE D.DocumentId=RequestID)
	and isnull(CertificateRequestId,'') NOT in(SELECT a.RequestId FROM tblCertificate a) 


END

GO
/****** Object:  StoredProcedure [dbo].[DCISUPDATECERT_APPROVAL]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[DCISUPDATECERT_APPROVAL](@CertificateID varchar(20))
AS BEGIN
UPDATE tblCertificate
SET IsSend = 'Y'
WHERE CertificateId = @CertificateID

END



GO
/****** Object:  StoredProcedure [dbo].[DCISupdateCertifiRqustHdr]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISupdateCertifiRqustHdr](
@RequestId varchar(20),    
@ModifiedBy varchar(50),   
@Consignor varchar(500), 
@Consignee varchar(500), 
@InvoiceNo varchar(50), 
@InvoiceDate datetime, 
@CountryCode varchar(20), 
@LoadingPort varchar(150), 
@PortOfDischarge varchar(150), 
@Vessel varchar(120), 
@PlaceOfDelivery varchar(150), 
@TotalInvoiceValue varchar(50), 
@TotalQuantity varchar(20),
@OtherComments varchar(150),
@OtherDetails varchar(250),
@SealRequired varchar(5)

)
as Begin
Update tblCertifcateRequestHeader
set 
    
Consignor = @Consignor,
Consignee = @Consignee, 
InvoiceNo = @InvoiceNo, 
InvoiceDate = @InvoiceDate, 
CountryCode = @CountryCode, 
LoadingPort = @LoadingPort, 
PortOfDischarge = @PortOfDischarge, 
Vessel = @Vessel, 
PlaceOfDelivery = @PlaceOfDelivery, 
TotalInvoiceValue = @TotalInvoiceValue, 
TotalQuantity= @TotalQuantity,
ModifiedDate = GETDATE(), 
ModifiedBy = @ModifiedBy,
OtherComments = @OtherComments,
OtherDetails = @OtherDetails,
SealRequired = @SealRequired
where RequestId = @RequestId


End 






GO
/****** Object:  StoredProcedure [dbo].[DCISupdateCertiReqDetail]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISupdateCertiReqDetail]
(
/**
 * Made By : Tharaka
 * Changed date : 12/7/2016
 * Change Done : @PackageType varchar(5) to (50)
 * Requested Date : 12/6/2016
 * Version No : 12.1
 * **/

 @SeqNo bigint,
 @GoodItem varchar(500), 
 @ShippingMark varchar(500), 
 @PackageType varchar(500), /**varchar(5)before**/
 @SummaryDesc varchar(1000), 
 @Quantity varchar(500), 
 @HSCode varchar(500)
 )

 as Begin 

 Update tblCertificateRequestDetails
 set  
 GoodItem = @GoodItem, 
 ShippingMark = @ShippingMark, 
 PackageType = @PackageType, 
 SummaryDesc = @SummaryDesc, 
 Quantity = @Quantity, 
 HSCode = @HSCode 
 Where SeqNo = @SeqNo

 End


GO
/****** Object:  StoredProcedure [dbo].[DCISupdateUserSignature]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[DCISupdateUserSignature](@UserID varchar(20),@PFXpath nvarchar(250), @SignatureIMGPath nvarchar(250), @CreatedBy varchar(20))
AS BEGIN
Update tblUserSignature  
Set PFXpath = @PFXpath, 
SignatureIMGPath = @SignatureIMGPath, 
CreatedBy = @CreatedBy, 
CreatedDate = GETDATE()
where UserID = @UserID

END


GO
/****** Object:  StoredProcedure [dbo].[DCISupdateUserSignatureDetails]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[DCISupdateUserSignatureDetails]
(@UserID varchar(20), 
@PFXpath nvarchar(250), 
@SignatureIMGPath nvarchar(250), 
@CreatedBy varchar(20))

as begin

Update tblUserSignature
Set
PFXpath = @PFXpath,
SignatureIMGPath = @SignatureIMGPath,
CreatedBy = @CreatedBy,
CreatedDate = GETDATE()
Where UserID = @UserID

End






GO
/****** Object:  StoredProcedure [dbo].[DCISUserSignatureIsSet]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[DCISUserSignatureIsSet](@UserID varchar(8))
as begin

SELECT * from tblUserSignature where UserID = @UserID

End




GO
/****** Object:  StoredProcedure [dbo].[DCSIcheckUserNCertificate]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[DCSIcheckUserNCertificate](@RequestID varchar(20),@CustomerID varchar(20))
as begin
select RequestId from tblCertifcateRequestHeader 
where CustomerId = @CustomerID
and RequestId = @RequestID

End






GO
/****** Object:  StoredProcedure [dbo].[getCitiesByCountry]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[getCitiesByCountry](@CountryID varchar(8))
as
Begin

select CityId,CityName from tblCity where CountryId like @CountryID

end







GO
/****** Object:  StoredProcedure [dbo].[getCountries]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[getCountries]
as
Begin

select * from tblCountry

end







GO
/****** Object:  StoredProcedure [dbo].[getCustomerRequesttest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[getCustomerRequesttest](@RequestId varchar(20),@Name varchar(100),@Telephone varchar(20),@Email varchar(20),@Fax varchar(20),@Address nvarchar(50),@TemplateId varchar(20))

as begin

select *
from tblCustomerRequest


End




GO
/****** Object:  StoredProcedure [dbo].[getCustomerTypes]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[getCustomerTypes]
as
Begin

select * from tblCustomerType

end







GO
/****** Object:  StoredProcedure [dbo].[getPendingRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getPendingRequest](@RequestStatusId varchar(8))
AS
BEGIN
select A.RequestDate,C.ReqType,A.RequestRefNo,A.CustomerId,B.CustomerName
from tblRequest A, tblcustomer B,tblRequestType C
where A.customerid=B.CustomerId
and A.ReqTypeId=C.ReqTypeId
and A.RequestStatusId=@RequestStatusId
order by RequestDate,A.ReqTypeId
END








GO
/****** Object:  StoredProcedure [dbo].[getRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getRequest](@RequestStatusId varchar(8))

AS

BEGIN

select ReqTypeId,A.RequestDate,B.CustomerId,CustomerName,RequestStatusId,RequestRefNo

from tblrequest A, tblCustomer B

where A.CustomerId=B.CustomerId

and RequestStatusId=@RequestStatusId;

END









GO
/****** Object:  StoredProcedure [dbo].[getRequestAdminName]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getRequestAdminName]
	
 (@AdminUserId varchar(20),@status varchar(10))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblCustomerRequest WHERE AdminUserId=@AdminUserId AND Status!=@status
END





GO
/****** Object:  StoredProcedure [dbo].[getRequestRowCount]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[getRequestRowCount]
as Begin

SELECT COUNT(*) + 1 as Total FROM tblRequest;

end







GO
/****** Object:  StoredProcedure [dbo].[getRequestTypes]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[getRequestTypes]

AS

BEGIN

select * from tblRequestType

END







GO
/****** Object:  StoredProcedure [dbo].[getSupportingDocumentForRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getSupportingDocumentForRequest](@RequestTypeId varchar(8))

AS

BEGIN

select A.DocumentId,DocumentName

from tblSupportingDocument A, tblSupportingDOCforRequest B

where A.DocumentId=B.DocumentId

and B.RequestTypeId=@RequestTypeId

END









GO
/****** Object:  StoredProcedure [dbo].[getSuppotingDocumentUpload]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[getSuppotingDocumentUpload](@RequestRefNo bigint)

AS

BEGIN

select RequestRefNo,A.DocumentId,DocumentName,Remarks,UploadedPath

from tblSupportingDocument A,tblSupportingDOCUpload B

where A.DocumentId=B.DocumentId

and B.RequestRefNo=@RequestRefNo

end









GO
/****** Object:  StoredProcedure [dbo].[getUserCategory]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[getUserCategory]

as begin

Select UserCategoryId,UserCategoryN
from tblUserCategory

End







GO
/****** Object:  StoredProcedure [dbo].[setCancledReson]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[setCancledReson](@ReasonCode varchar(10),@Reason varchar(100))
as 
begin
Insert Into tblCanceldReason
(ReasonCode,Reason)
values
(@ReasonCode,@Reason)
End







GO
/****** Object:  StoredProcedure [dbo].[setCustomerID]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[setCustomerID]
as
begin
select  COUNT(*) + 1 as Total from tblCustomer

end







GO
/****** Object:  StoredProcedure [dbo].[setDCISUserGroup]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[setDCISUserGroup](@GroupId varchar(20),@GroupName nvarchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
insert into tblUserGroup
(GroupId, GroupName, CreatedDate, IsActive, CreatedBy)
values
(@GroupId,@GroupName,getdate(),@IsActive,@CreatedBy)


END






GO
/****** Object:  StoredProcedure [dbo].[setRequest]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[setRequest](@ReqTypeId varchar(8), @CustomerId varchar(8), 
@RequestStatusId varchar(8),
@RequestRefNo bigint)

as Begin

insert into tblRequest
(ReqTypeId, RequestDate, CustomerId, RequestStatusId, RequestRefNo)
values 
(@ReqTypeId,GETDATE(),@CustomerId,@RequestStatusId,@RequestRefNo)

End







GO
/****** Object:  StoredProcedure [dbo].[updateCustomerrequestStatus]    Script Date: 5/18/2018 12:56:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[updateCustomerrequestStatus](@Requestid bigint,@status varchar(2))

AS

BEGIN

update tblCustomerRequest

set status=@status

where Requestid=@Requestid

END







GO
