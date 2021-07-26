const input = document.querySelector('.searchInput')

input.addEventListener('keyup', () => {
    let searchText = $("#searchKeyword").val();
    var param = { keyword: searchText };
    if (searchText != null && searchText != undefined) {
        ajaxPost("/Search", param, null, null, function (data) {
            console.log(data);
            let noticeHtml = '<tr><th style="width: 10%">글번호</th><th style="width: 65%">제목</th><th style="width: 10%">작성자</th><th style="width: 15%">작성일</th></tr>';

            for (let i = 0; i < data.length; i++) {
                noticeHtml += '<tr class="list">';
                noticeHtml += '<td>' + data[i].num + '</td>';
                noticeHtml += '<td style="text-align: left; padding-left: 15px;">' + data[i].title + '</td>';
                noticeHtml += '<td>' + data[i].userId + '</td>';
                if (data[i].iDate != "") {
                    noticeHtml += '<td>' + parseJsonDate(data[i].iDate).format("yyyy-MM-dd")  + '</td>';
                } else {
                    noticeHtml += '<td>&nbsp;</td>';
                }
                noticeHtml += '</tr>';
            }
            $('.tbl_list').html(noticeHtml);
        });
    }
})


// Json 날짜 형식 변환
function parseJsonDate(value) {
    if (value == "" || value == null || value == "undefind") {
        return "";
    }
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(value);
    var dt = new Date(parseFloat(results[1]));

    return dt;
};

//ajax 포스트
function ajaxPost(url, param, type, userState, fnSuccess, fnFail, async) {
    var posturl = _url;
    posturl += "/" + url;

    if (!async) {
        async = false;
    }

    $.ajax({
        type: "POST",
        url: posturl,
        data: param,
        //timeout: 3000,
        dataType: "json",
        userType: type,
        userState: userState,
        async: async,
        success: function (data, status) {
            if (fnSuccess != null)
                fnSuccess(data, status, this.userType, this.userState);
        },
        error: function (xhr, status, error) {
            if (fnFail != null)
                fnFail(xhr, status, error, this.userType, this.userState);
        }
    });
}


// javascript Date를 포멧팅. 예) date.format('yyyy-MM-dd');
Date.prototype.format = function (f) {
    if (!this.valueOf()) return " ";
    var weekName = ["일요일", "월요일", "화요일", "수요일", "목요일", "금요일", "토요일"];
    var d = this;
    return f.replace(/(yyyy|yy|MM|dd|E|hh|mm|ss|a\/p|M|d)/gi, function ($1) {
        switch ($1) {
            case "yyyy": return d.getFullYear();
            case "yy": return (d.getFullYear() % 1000).zf(2);
            case "MM": return (d.getMonth() + 1).zf(2);
            case "dd": return d.getDate().zf(2);
            case "M": return (d.getMonth() + 1).toString();
            case "d": return d.getDate().toString();
            case "E": return weekName[d.getDay()];
            case "HH": return d.getHours().zf(2);
            case "hh": return ((h = d.getHours() % 12) ? h : 12).zf(2);
            case "mm": return d.getMinutes().zf(2);
            case "ss": return d.getSeconds().zf(2);
            case "a/p": return d.getHours() < 12 ? "오전" : "오후";
            default: return $1;
        }
    });
};

String.prototype.string = function (len) { var s = '', i = 0; while (i++ < len) { s += this; } return s; };
String.prototype.zf = function (len) { return "0".string(len - this.length) + this; };
Number.prototype.zf = function (len) { return this.toString().zf(len); };