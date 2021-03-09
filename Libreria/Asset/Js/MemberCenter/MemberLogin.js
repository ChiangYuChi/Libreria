window.onload = () => {
    if (memberName != '' && memberPassword != '') {
        document.getElementById("MemberName").setAttribute("value", memberName);
        document.getElementById("MemberPassword").setAttribute("value", memberPassword);
        document.getElementById("Remember").click();
    }
}

