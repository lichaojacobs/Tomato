function showHint(str)
{
    var xmlhelper
    if (str.length == 0)
    {
        document.getElementById("txtHint").innerHTML = "";
        return;
    }
    if (window.XMLHttpRequest) {
        xmlhelper = new XMLHttpRequest();
    }
    else {
        xmlhelper = new ActiveXObject("Microsoft.XMLHTTP");
    }
    xmlhelper.onreadystatechange = function ()
    {
        if (xmlhelper.readyState == 4 && xmlhelper.status == 200)
        {
            document.getElementById("txtHint").innerHTML = xmlhelper.responseText;
        }
        xmlhttp.open("GET", "gethint.asp?q=" + str, true);
        xmlhttp.send();
    }
    


}