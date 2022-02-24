LẬP TRÌNH WINDOWS - PROJEC2 2 - SimplePaint


======================================= Báo cáo ====================================================

1. Thông tin cá nhân
MSSV: 19120347
Họ Tên: Trần Ngọc Sang
Email sinh viên: 19120347@student.hcmus.edu.vn
Email cá nhân: ngocsang30032001@gmail.com


2. Mức độ hoàn thành:
    a. Core requirements
    *  Các phần đã làm được:
        - Nộp động các đối tượng 
        - Người dùng chọn đối tượng để vẽ
        - Người dùng xem preview đối tượng trước họ muốn vẽ
        - Sau khi vẽ hình vẽ tồn tại vĩnh viễn (trừ khi người dùng thay đổi)
        - Lưu các đối tượng đã vẽ dưới dạng file nhị phân và có vể mở lại để vẽ tiếp
        - Lưu các đối tượng đã vẽ dưới dạng hình ảnh PNG/JPEG    

    * Phần chưa làm được
    - Load hình ảnh đã lưu (PNG/JPEG) lên phần mềm

    b. Improvements:
    * Các phần đã làm được:
    - Cho phép người dùng thay đổi màu viền, đồ dày viền, kiểu viền theo các dạng đã cho sẵn
    - Thay đổi đối tượng sau khi vẽ
    - Tô màu nền cho các đối tượng


    c. Các đối tượng vé
    - Đủ 3 hình cơ bản: Line, Rectangle, Ellipse
    - Mở rộng thêm: Circle, Square


3. Tự đánh giá
    a. Ưu điểm:
    - Dễ sử dụng
    - Giao diện tương đối đẹp
    - Hoàn thành gần đủ Core requirements,
    - Thêm được 3 improvements
    - Mở rộng thêm 2 hình: Circle, Square
        
    b. Nhược điểm:
    - Chưa Lưu các đối tượng đã vẽ dưới dạng hình ảnh PNG/JPEG   
    - Load hình ảnh đã lưu (PNG/JPEG) lên phần mềm 
    - Trong phần thiết kế class, interface IShape có cá thuộc tính như Fill (thừa so Line, Point)

    c. Hướng phát triển:
    - Thêm các giao diện điều khiển riêng cho mỗi đối tượng:
    VD: Phần thay đổi Fill sẽ không xuất hiện nếu đối tượng là Line
    - Thêm đối tượng Image để chứa hình ảnh sau khi load lên
    - Cho phép lựa chọn để sửa/ xóa đối tượng sau khi vẽ.

    d. Thời gian thực hiện: 26 tiếng
    e. Điểm tự đánh giá: 9/10


4. Hướng dẫn sử dụng:
    - Thêm các file dll của đối tượng vào thư mục Release/shapeDllFiles.
    - Chạy file SimplePaint.exe để chạy chương trình
    - Nếu không tồn tại thư mục shapeDllFiles thì tự tạo hoặc chạy file SimplePaint.exe, chương trình sẽ tự tạo thư mục shapeDllFiles và thêm file Line2D.dll của đối tượng mặc định (Line2D)

    - Các file Rectangle2D.dll, Cirle2D.dll, Ellipse2D.dll, Square.dll
    đã được thêm sẵn vào thư mục 19120347/Dll_Files. Chỉ cần copy vào thư mục Release/shapeDllFiles (nếu nó bị xóa)

5. Video Demo:
    - Youtube:
    https://youtu.be/RWk-CiqlaJc

    - Link drive (dự phòng)
    https://drive.google.com/drive/folders/1DrzPUPGwzttXKwuKs96pDdfstZV8Iiq-?usp=sharing



6. Tài liệu tham khảo:
https://github.com/tqbdev/paint-nmcnpm.git
https://www.c-sharpcorner.com/uploadfile/hrojasara/how-to-read-and-write-binary-file-in-C-Sharp/?fbclid=IwAR1KGQ43br5enMXplYCsV7tAi99xjMVvMPlIjA1tiGAfcXCqSZb8iHfrvjI
http://csharphelper.com/blog/2019/05/make-an-image-containing-shadowed-text-in-wpf-and-c/?fbclid=IwAR321yvd_U0oCACrfVmyLOZHPsQWpJubBIDj85WgqmYZPh72-kmMqIARnR8
https://laptrinhvb.net/bai-viet/devexpress/---Csharp----Huong-dan-ma-hoa-hinh-anh-sang-Base64-va-nguoc-lai/b06a68c6c5f71141.html
https://stackoverflow.com/questions/21325661/convert-an-image-selected-by-path-to-base64-string


======================================= DEMO ====================================================    

* Sau khi chạy file SimplePaint.exe trong thư mục Release

1. Chọn button của hình muốn vẽ, tên đối tượng được chọn hiển thị ở phần title

2. Sau khi vẽ xong (thả chuột), có thể chỉnh sửa lại đối tượng
(nếu muốn vẽ tiếp thì click chuột xuống nền để mất đi khung select đối tượng)

3. Thay đổi outline, fill, size, style của đối tượng ở các button tương ứng

4. Nhấn Save để để chọn file để lưu

5. Có thể Save As với tên khác

6. Load lại file đã lưu

7. Export ra file ảnh:
    - PNG
    - JPEG

