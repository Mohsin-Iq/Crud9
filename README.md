# Crud9
 <script type="text/javascript">
        $(document).ready(function(){

            $.ajax({
                type:"POST",
                url: '@Url.Action("Create", "Employee")',
                dataType:"Json",
                contentType: "application/json; charset=utf-8",
                success: function (resopnse){
                    $(".table-summary .tbody");
                    for (var i = 0; i < resopnse.length; i++) {
                        var br = resopnse[i];
                        var str = '<tr><td>' + br.name + '</td><td>' + br.email + '</td><td>' + br.phone + '</td></tr>';
                        $('.table-summary').append(str);
                    }
                },
                Error: function(){
                    alert:("Error ");
                }
            });
        });
      
    </script>
