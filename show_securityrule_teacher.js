function show_btn_sr(){
    var flag = false;
    var UserRules = Xrm.Utility.getGlobalContext().userSettings.securityRoles;
    var ValidRule = "882f36c6-cfbf-ed11-af06-005056be2711"; // teacher Security Rule
    for (var i = 0; i < UserRules.length; i++) {
        if(UserRules[i] === ValidRule){
            flag = true;
        }
    }
    return flag;
}