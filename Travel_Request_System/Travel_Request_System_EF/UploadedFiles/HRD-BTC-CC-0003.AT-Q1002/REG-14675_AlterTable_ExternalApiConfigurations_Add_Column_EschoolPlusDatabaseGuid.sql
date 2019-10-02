/*

REG-14675: Internal specialist should be able to enable Admin SSO

DESCRIPTION:

School staff user should be able to authenticate into Enrollment from eSP SIS
when relevant External API Configuration is not on top or
when there are multiple External API Configurations with SSO enabled


*/


declare @procPhase nvarchar(32);
declare @error nvarchar(4000);
declare @ErrorMessage nvarchar(4000);
declare @ErrorSeverity int;
declare @ErrorState int;

set @error = '';
set @procPhase = '';

begin try

IF NOT EXISTS
    (
        SELECT 1
        FROM INFORMATION_SCHEMA.COLUMNS
        WHERE COLUMN_NAME = 'EschoolPlusDatabaseGuid'
        AND TABLE_NAME = 'ExternalApiConfigurations'
        AND TABLE_SCHEMA = 'dbo'
    )
BEGIN
    set @procPhase = 'ADD COLUMN [ExternalApiConfigurations].[EschoolPlusDatabaseGuid]';
    EXEC('alter TABLE dbo.ExternalApiConfigurations add EschoolPlusDatabaseGuid nvarchar(400)')
END
end try

begin catch
    select
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE();
        
    set @error = 'ERROR OCCURRED AT: ' + @procPhase + 'Error Message:
  ' + @ErrorMessage;

    raiserror(@error, @ErrorSeverity, @ErrorState);
end catch
