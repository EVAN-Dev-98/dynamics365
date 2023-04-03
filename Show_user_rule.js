function show_team(){
    var UserRule = Xrm.Utility.getGlobalContext().userSettings.securityRoles;
    console.log(UserRule);
}