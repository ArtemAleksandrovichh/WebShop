
const ProfileViews = { PERSONAL: "Personal", INDEX: "Index" }



function GetView(viewName) {
    $('#menu-content').load('/Profile/GetView', { viewName: viewName });
}


