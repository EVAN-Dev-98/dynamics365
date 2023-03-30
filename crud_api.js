function create_class(executionContext){
    let formContext = executionContext.getFormContext();
    let btn = formContext.getAttribute("ev_class_add").getValue();
    let new_name = formContext.getAttribute("ev_class_name_add").getValue();
    let new_capacity = formContext.getAttribute("ev_class_capacity_add").getValue();
    
    if(btn){
        if(new_name === null || new_capacity === null){
            alert("مقدار نام کلاس و ظرفیت کلاس را وارد کنید!");
            formContext.getAttribute("ev_class_add").setValue(false);
        }
        else{
            var data = 
            {
                "new_name": new_name,
                "new_capacity": new_capacity
            }
            Xrm.WebApi.createRecord('new_class', data).then(
                function success(result) {
                    console.log(result);
                    formContext.getAttribute("ev_class_name_add").setValue(null);
                    formContext.getAttribute("ev_class_capacity_add").setValue(null);
                    formContext.getAttribute("ev_class_add").setValue(false);
                },
                function (error) {
                    console.log(error.message);
                }
            );
        }
    }
}


function update_class(executionContext){
    let formContext = executionContext.getFormContext();
    let lookup_class = formContext.getAttribute("ev_find_class_update").getValue();
    
    let btn = formContext.getAttribute("ev_class_update").getValue();
    let new_name_up = formContext.getAttribute("ev_class_name_update").getValue();
    let new_capacity_up = formContext.getAttribute("ev_class_capacity_update").getValue();


    if(btn){
        if(lookup_class === null || new_name_up === null || new_capacity_up === null){
            alert("مقدار کلاس ، نام جدید کلاس ، ظرفیت جدید کلاس را وارد کنید!");
            formContext.getAttribute("ev_class_update").setValue(false);
        }
        else{
            var id = lookup_class[0].id;
            var data = 
            {
                "new_name": new_name_up,
                "new_capacity": new_capacity_up
            }
            Xrm.WebApi.updateRecord('new_class', id, data).then(
                function success(result) {
                    console.log(result);
                    formContext.getAttribute("ev_class_name_update").setValue(null);
                    formContext.getAttribute("ev_class_capacity_update").setValue(null);
                    formContext.getAttribute("ev_class_update").setValue(false);
                    formContext.getAttribute("ev_find_class_update").setValue(null);
                },
                function (error) {
                    console.log(error.message);
                }
            );
        }
    }
}


function delete_class(executionContext){
    let formContext = executionContext.getFormContext();
    let lookup_class = formContext.getAttribute("ev_find_class_delete").getValue();
    let btn = formContext.getAttribute("ev_class_delete").getValue();

    if(btn){
        if(lookup_class === null){
            alert("مقدار کلاس را وارد کنید!");
            formContext.getAttribute("ev_class_delete").setValue(false);
        }
        else{
            var id = lookup_class[0].id;
            Xrm.WebApi.deleteRecord('new_class', id).then(
                function success(result) {
                    console.log(result);
                    formContext.getAttribute("ev_class_delete").setValue(false);
                    formContext.getAttribute("ev_find_class_delete").setValue(null);
                    
                },
                function (error) {
                    console.log(error.message);
                }
            );
        }
    }
}

function view_class(executionContext){
    let formContext = executionContext.getFormContext();
    let lookup_class = formContext.getAttribute("ev_find_class").getValue();
    if (lookup_class !== null){
        var id = lookup_class[0].id;
        Xrm.WebApi.retrieveRecord('new_class', id).then(
            function success(result) {
                console.log(result);
                formContext.getAttribute("ev_view_class_name").setValue(result.new_name);
                formContext.getAttribute("ev_view_class_capacity").setValue(result.new_capacity);
            },
            function (error) {
                console.log(error.message);
            }
        );
    }
}