window.onload = () => {
    if (memberName != null && memberPassword != null) {
        document.getElementById("MemberName").setAttribute("value", memberName);
        document.getElementById("MemberPassword").setAttribute("value", memberPassword);
        document.getElementById("Remember").click();
    }

}

