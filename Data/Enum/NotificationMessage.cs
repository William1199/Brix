using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enum
{
    public static class NotificationMessage
    {
        public static string SAVENOTI = "đã thêm bài đăng của bạn vào mục yêu thích";
        public static string COMMITMENTNOTI = "cam kết đã được tạo thành công";
        public static string APPLIEDNOTI = "đã đăng kí vào công việc";
        public static string SEND_INVITE = "đã gửi lời mời đăng kí vào công việc";
        public static string UPDATE_COMMIMENT = "đã cập nhật trạng thái cam kết thành công";
        public static string CREATE_BILL = "có một đơn hàng mới ";
        public static string UPDATE_BILL_ACCEPTED = "đã xác nhận đơn hàng ";
        public static string UPDATE_BILL_DELIVERD = "đã chuyển sang trạng thái vận chuyển đơn hàng ";
        public static string UPDATE_BILL_RECEIVED = "đã chuyển sang trạng thái nhận đơn hàng thành công";
        public static string UPDATE_BILL_CANCELED = "đã chuyển sang trạng thái hủy";
        public static string REPORT_PRODUCT = "đã báo cáo sản phẩm của bạn";
        public static string REPORT_POST = "đã báo cáo bài viết của bạn";
    }

}
