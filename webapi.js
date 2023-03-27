let All_class = [];
let Contact_class = [];

function fetchXML(executionContext){
    let formContext = executionContext.getFormContext();
    let class_name = formContext.getAttribute("ev_class").getValue()[0].name;
    const fetchXML = "?fetchXml=<fetch mapping='logical' distinct='false'><entity name='new_class'><attribute name='new_name'/></entity></fetch>";

    Xrm.WebApi.online.retrieveMultipleRecords("new_class", fetchXML).then(
        function success(result) {
            for (var i = 0; i < result.entities.length; i++) {
                All_class.push(result.entities[i].new_name);
                if(result.entities[i].new_name == class_name){
                    Contact_class.push(result.entities[i].new_name);
                }
            }
            show_data();
        },
        function (error) {
            console.log(error.message);
        }
    );
}

function show_data(){
    console.log("لیست تمامی کلاس ها بوسیله FetchXml :");
    console.log(All_class);
    console.log("کلاس این کاربر :");
    console.log(Contact_class);
    console.log("-----------------");
}

/* ---------------------------------------------------------------------------------- */

let All_class_2 = [];
let Contact_class_2 = [];

function xmlHttpReq(executionContext){
    let formContext = executionContext.getFormContext();
    let class_name = formContext.getAttribute("ev_class").getValue()[0].name;
    const fetchXML = "<fetch><entity name='new_class'><attribute name='new_name'/></entity></fetch>";
    var encodedFetchXML = encodeURIComponent(fetchXML);
    var Request = new XMLHttpRequest();
    Request.open("get", Xrm.Page.context.getClientUrl() + "/api/data/v9.0/new_classes?fetchXml=" + encodedFetchXML , true);
    Request.setRequestHeader("OData-MaxVersion", "4.0");
    Request.setRequestHeader("OData-Version", "4.0");
    Request.setRequestHeader("Accept", "application/json");
    Request.setRequestHeader("Prefer", "odata.include-annotations=\"*\"");
    Request.onreadystatechange = function() {
        if(this.readyState == 4){
            Request.onreadystatechange = null;
            if(this.status == 200){
                var result = JSON.parse(this.response);
                for (var i = 0; i < result.value.length; i++) {
                    All_class_2.push(result.value[i].new_name);
                    if(result.value[i].new_name == class_name){
                        Contact_class_2.push(result.value[i].new_name);
                    }
                }
                show_data_2();
            }
            else {
                Xrm.Utility.alertDialog(this.statusText);
            }
        }
    };
    Request.send();
}

function show_data_2(){
    console.log("-----------------");
    console.log("لیست تمامی کلاس ها بوسیله XMLHttpRequest :");
    console.log(All_class_2);
    console.log("کلاس این کاربر :");
    console.log(Contact_class_2);
}