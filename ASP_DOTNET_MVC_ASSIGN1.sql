Create Procedure PROC_SELECT_BRANDS_MVC
AS
BEGIN

SELECT brand_id, brand_name from brands;
END

/////////////////////////////////////////////////////////////////////////

Create Procedure PROC_SELECT_CATEGORIES_MVC
AS
BEGIN

SELECT category_id, category_name from categories;
END

EXEC PROC_SELECT_CATEGORIES_MVC





