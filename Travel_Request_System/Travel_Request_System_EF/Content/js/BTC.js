$(document).ready(function () {
            var errtext = "@Html.Raw(@ViewBag.ErrorMessage)";
            if (errtext && errtext.trim().length) {
                $(function () {
                    $('#modal-danger').appendTo("body").modal('show');
                });
            }
            $(function () {
                $('#detailsGrid1').DataTable({
                    'paging': true,
                    'lengthChange': false,
                    'searching': false,
                    'ordering': true,
                    'info': true,
                    'autoWidth': true
                })
            })

            $(function () {
                $('.select2').select2()
            })

            $('.timepicker').timepicker({
                showInputs: false
            })

        });