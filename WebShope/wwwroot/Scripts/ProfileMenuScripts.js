
const ProfileViews = {PERSONAL:"Personal", INDEX:"Index"}

function GetView(_viewName) {
    $('#menu-content').load('/Profile/GetView', { viewName: _viewName });
}


