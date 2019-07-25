CREATE PROCEDURE [dbo].[usp_GetCarPriceRangesCount]
	@cars BaseCars READONLY
AS
SELECT Range AS Value, COUNT(*) AS CarsCount, Range AS Text FROM (
	SELECT
		CASE 
			WHEN Price >= 10.000 AND Price <= 39.999 THEN '10.000 - 39.999'
			WHEN Price >= 40.000 AND Price <= 49.999 THEN '40.000 - 49.999'
			WHEN Price >= 50.000 AND Price <= 59.999 THEN '50.000 - 59.999'
			WHEN Price >= 60.000 AND Price <= 69.999 THEN '60.000 - 69.999' 
			WHEN Price >= 70.000 AND Price <= 79.999 THEN '70.000 - 79.999' 
			WHEN Price >= 80.000 AND Price <= 89.999 THEN '80.000 - 89.999' 
			WHEN Price >= 100.000 AND Price <= 149.999 THEN '100.000 - 149.999' 
		END AS Range
	FROM @cars
) AS t
GROUP BY Range
RETURN 0