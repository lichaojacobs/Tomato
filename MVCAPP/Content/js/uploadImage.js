function uploadimage() {
    //�ϴ�ʱ���ֵĻ���ͼƬ��ʵ���û������Ѻ�    
$.ajaxFileUpload({
        type: 'post',
        url: '/Common/UpLoadImage',
        secureuri: false,
        fileElementId: 'image',
        dataType: 'text/html',
        success: function (data, textstatus) {


            document.getElementById("showimage").style.display = "block";
            $("#imagebox").attr("src", data);
            //var textbox = document.getElementById("imageAddress");
            //textbox.value = data;
            $("#imageAddress")[0].value = data;
            


        },
        error: function (data, status) {

            alert("ERROR:" + data);

        }



    })


}