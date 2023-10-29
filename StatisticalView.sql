
Create proc sp_Statistical
as
BEGIN
	DECLARE @SoTruyCapGanNhat int
	DECLARE @Count int
	SELECT @Count=COUNT(*) FROM Statistical
	IF @Count IS NULL SET @Count=0
	IF @Count=0
		INSERT INTO Statistical(Time,NumberOfHits)
		Values(GETDATE(),1)
	ELSE
		BEGIN
			DECLARE @ThoiGianGanNhat datetime
			SELECT @SoTruyCapGanNhat=tk.NumberOfHits,@ThoiGianGanNhat=tk.Time FROM Statistical tk
			WHERE tk.id=(SELECT MAX(tk1.Id) FROM Statistical tk1)
			IF @SoTruyCapGanNhat IS NULL SET @SoTruyCapGanNhat=0
			IF @ThoiGianGanNhat IS NULL SET @ThoiGianGanNhat=GETDATE()
			--Nếu chuyển sang ngày  mới chưa (sau 12h đêm)
			-- nếu chưa chuyển sang ngày mới thì update
			IF DAY(@ThoiGianGanNhat)=DAY(GETDATE())
				BEGIN
					UPDATE Statistical
					SET 
					NumberOfHits=@SoTruyCapGanNhat+1,
					Time=GETDATE()
					WHERE Id=(SELECT MAX(tk1.Id) FROM Statistical tk1)
				END
			-- Nếu đã sang ngày mới rồi thì chúng thêm 1 bản ghi mới
			ELSE
				INSERT INTO Statistical(Time,NumberOfHits)
				Values(GETDATE(),1)
		END

			-- Thống kê Hom nay, Hom qua, Tuan nay, Tuan Truoc, Thang nay, Thang Truoc
		DECLARE @HomNay int
		SET @HomNay=DATEPART(DW,GETDATE());
		SELECT @SoTruyCapGanNhat=NumberOfHits,@ThoiGianGanNhat=Time FROM Statistical
		WHERE Id=(SELECT MAX(Id) FROM Statistical)

		--so truy cap ngay hqua
		DECLARE @TruyCapHomQua bigint
		SELECT @TruyCapHomQua=ISNULL(NumberOfHits,0) FROM Statistical 
		Where CONVERT(nvarchar(20),Time,103)=CONVERT(nvarchar(20),GETDATE()-1,103)
		IF @TruyCapHomQua IS NULL SET @TruyCapHomQua=0

		--truy cap dau tuan này
		DECLARE @DauTuanNay datetime
		SET @DauTuanNay= DATEADD(wk, DATEDIFF(wk, 6, GetDate()), 6)
		-- Tính Ngày đầu của tuần trước Tính từ thời điểm 00:00:))
		DECLARE @NgayDauTuanTruoc datetime
		SET @NgayDauTuanTruoc = Cast(CONVERT(nvarchar(30),DATEADD(dd, -(@HomNay+6), GetDate()),101) AS datetime)
		-- Tính ngày cuối tuần trước tính đến 24h ngày cuối tuần 
		DECLARE @NgayCuoiTuanTruoc datetime
		SET @NgayCuoiTuanTruoc = Cast(CONVERT(nvarchar(30),DATEADD(dd, -@HomNay, GetDate()),101) +' 23:59:59' AS datetime)

		-- so truy cap tuan nay
		DECLARE @TruyCapTuanNay bigint
		SELECT @TruyCapTuanNay=ISNULL(sum(NumberOfHits),0) FROM Statistical
		Where Time BETWEEN @DauTuanNay AND GETDATE()

		-- Tính số truy cập tuần trước
		DECLARE @SoLanTruyCapTuanTruoc bigint
		SELECT @SoLanTruyCapTuanTruoc=isnull(sum(NumberOfHits),0)  FROM Statistical ttk 
			WHERE ttk.Time BETWEEN @NgayDauTuanTruoc AND @NgayCuoiTuanTruoc
		
		-- Tính số truy cập tháng này
		DECLARE @SoTruyCapThangNay bigint 
		SELECT @SoTruyCapThangNay=isnull(Sum(NumberOfHits),0)
			FROM Statistical ttk WHERE MONTH(ttk.Time)=MONTH(GETDATE())
		
		-- Tính số truy cập tháng trước
		DECLARE @SoTruyCapThangTruoc bigint 
		SELECT @SoTruyCapThangTruoc=isnull(Sum(NumberOfHits),0)
			FROM Statistical ttk WHERE MONTH(ttk.Time)=MONTH(GETDATE())-1
		
		-- Tính tổng số
		DECLARE @TongSo bigint
		SELECT  @TongSo=isnull(Sum(NumberOfHits),0) FROM Statistical ttk
		
		SELECT @SoTruyCapGanNhat AS HomNay, 
		@TruyCapHomQua AS HomQua,
		@TruyCapTuanNay AS TuanNay, 
		@SoLanTruyCapTuanTruoc AS TuanTruoc, 
		@SoTruyCapThangNay AS ThangNay, 
		@SoTruyCapThangTruoc AS ThangTruoc,
		@TongSo AS TatCa
END

