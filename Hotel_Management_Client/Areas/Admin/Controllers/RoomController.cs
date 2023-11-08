using Hotel_Management_API.DTO;
using Hotel_Management_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Hotel_Management_Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoomController : Controller
    {
        private readonly HttpClient client;
        private string _urlApi = "https://localhost:7083/api/Rooms";

        public RoomController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        // GET: RoomController
        public async Task<IActionResult> Index()
        {

            HttpResponseMessage response = await client.GetAsync(_urlApi + "/GetRoom");
            string strData = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<RoomDTO> listRooms = JsonSerializer.Deserialize<List<RoomDTO>>(strData, options);
            return View(listRooms);

        }

        // GET: RoomController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await client.GetAsync($"{_urlApi}/GetRoom/{id}");

            if (response.IsSuccessStatusCode)
            {
                var room = await response.Content.ReadFromJsonAsync<RoomDTO>();
                ViewBag.TypeName = room.TypeName;
                ViewBag.StatusName = room.StatusName;
                ViewBag.FloorName = room.FloorName;
                return View(room);
            }
            else
            {
                // Xử lý lỗi nếu cần
                return View();
            }
        }

        // GET: RoomController/Create
        public async Task<IActionResult> Create()
        {
            // Lấy danh sách các tầng (Floors), loại phòng (RoomTypes), trạng thái (Status) từ API
            var floorsResponse = await client.GetAsync("https://localhost:7083/api/Floors");
            var roomTypesResponse = await client.GetAsync("https://localhost:7083/api/RoomTypes");
            var statusResponse = await client.GetAsync("https://localhost:7083/api/Status");

            if (floorsResponse.IsSuccessStatusCode && roomTypesResponse.IsSuccessStatusCode && statusResponse.IsSuccessStatusCode)
            {
                var floors = await floorsResponse.Content.ReadFromJsonAsync<List<Floor>>();
                var roomTypes = await roomTypesResponse.Content.ReadFromJsonAsync<List<RoomType>>();
                var status = await statusResponse.Content.ReadFromJsonAsync<List<Status>>();

                // Gán danh sách Floors, RoomTypes, và Status vào ViewBag
                ViewBag.Floors = new SelectList(floors, "FloorId", "FloorName");
                ViewBag.RoomTypes = new SelectList(roomTypes, "TypeId", "Name");
                ViewBag.Status = new SelectList(status, "StatusId", "StatusName");

                return View();
            }
            else
            {
                // Xử lý lỗi nếu cần
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            // Gửi yêu cầu POST để tạo phòng
            var response = await client.PostAsJsonAsync(_urlApi + "/Create", room);

            if (response.IsSuccessStatusCode)
            {
                //return View("/Areas/Admin/Views/Room/Index.cshtml");
                TempData["SuccessMessage"] = "Room created successfully.";
                return RedirectToAction("Index", "Room", new { area = "Admin" });
            }
            else
            {
                // Xử lý lỗi nếu cần
                return View();
            }
            return View();
        }

        // GET: RoomController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await client.GetAsync($"{_urlApi}/GetRoom/{id}");

            if (response.IsSuccessStatusCode)
            {
                var room = await response.Content.ReadFromJsonAsync<Room>();

                // Lấy danh sách Floors, RoomTypes, Status từ API
                var floorsResponse = await client.GetAsync("https://localhost:7083/api/Floors");
                var roomTypesResponse = await client.GetAsync("https://localhost:7083/api/RoomTypes");
                var statusResponse = await client.GetAsync("https://localhost:7083/api/Status");

                if (floorsResponse.IsSuccessStatusCode && roomTypesResponse.IsSuccessStatusCode && statusResponse.IsSuccessStatusCode)
                {
                    var floors = await floorsResponse.Content.ReadFromJsonAsync<List<Floor>>();
                    var roomTypes = await roomTypesResponse.Content.ReadFromJsonAsync<List<RoomType>>();
                    var status = await statusResponse.Content.ReadFromJsonAsync<List<Status>>();

                    // Đảm bảo rằng danh sách Floors, RoomTypes, Status đã được lấy từ API

                    ViewBag.Floors = new SelectList(floors, "FloorId", "FloorName");
                    ViewBag.RoomTypes = new SelectList(roomTypes, "TypeId", "Name");
                    ViewBag.Status = new SelectList(status, "StatusId", "StatusName");

                    return View(room);
                }
            }
            // Xử lý lỗi nếu cần
            return View();
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            var response = await client.PutAsJsonAsync(_urlApi + $"/Update/{id}", room);

            if (response.IsSuccessStatusCode)
            {
                return View("/Areas/Admin/Views/Room/Index.cshtml");
            }
            else
            {
                // Xử lý lỗi nếu cần
                return View();
            }
        }


        // GET: RoomController/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var response = await client.GetAsync($"{_urlApi}/GetRoom/{id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var room = await response.Content.ReadFromJsonAsync<Room>();
        //        return View(room);
        //    }
        //    else
        //    {
        //        // Xử lý lỗi nếu cần
        //        return View();
        //    }
        //}

        //// POST: RoomController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var response = await client.DeleteAsync($"{_urlApi}/Delete/{id}");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        // Xử lý lỗi nếu cần
        //        return View();
        //    }
        //}
        // POST: RoomController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await client.DeleteAsync($"{_urlApi}/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Xóa thành công, bạn có thể thực hiện điều gì đó ở đây, ví dụ: đặt TempData thông báo.
            }
            else
            {
                // Xử lý lỗi nếu cần
            }

            // Sau khi xóa xong, bạn có thể chuyển hướng trở lại trang Index.
            //return View("/Areas/Admin/Views/Room/Index.cshtml");
            return RedirectToAction(nameof(Index));
            //return RedirectToAction("Index", "Room", new { area = "Admin" });
        }
    }
}
