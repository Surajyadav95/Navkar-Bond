$(document).ready(function(){$("#demo-cls-wz").bootstrapWizard({tabClass:"wz-classic",nextSelector:".next",previousSelector:".previous",onTabClick:function(a,b,c){return!1},onInit:function(){$("#demo-cls-wz").find(".finish").hide().prop("disabled",!0)},onTabShow:function(a,b,c){var d=b.find("li").length,e=c+1,f=e/d*100;$("#demo-cls-wz").find(".progress-bar").css({width:f+"%"}),e>=d?($("#demo-cls-wz").find(".next").hide(),$("#demo-cls-wz").find(".finish").show(),$("#demo-cls-wz").find(".finish").prop("disabled",!1)):($("#demo-cls-wz").find(".next").show(),$("#demo-cls-wz").find(".finish").hide().prop("disabled",!0))}}),$("#demo-bv-wz").bootstrapWizard({tabClass:"wz-steps",nextSelector:".next",previousSelector:".previous",onTabClick:function(a,b,c){return!1},onInit:function(){$("#demo-bv-wz").find(".finish").hide().prop("disabled",!0)},onTabShow:function(a,b,c){var d=b.find("li").length,e=c+1,f=c/d*100,g=100/d/2;$("#demo-bv-wz").find(".progress-bar").css({width:f+"%",margin:"0px "+g+"%"}),b.find("li:eq("+c+") a").trigger("focus"),e>=d?($("#demo-bv-wz").find(".next").hide(),$("#demo-bv-wz").find(".finish").show(),$("#demo-bv-wz").find(".finish").prop("disabled",!1)):($("#demo-bv-wz").find(".next").show(),$("#demo-bv-wz").find(".finish").hide().prop("disabled",!0))},onNext:function(){if(a=null,$("#demo-bv-wz-form").bootstrapValidator("validate"),a===!1)return!1}});var a;$("#demo-bv-wz-form").bootstrapValidator({message:"This value is not valid",feedbackIcons:{valid:"fa fa-check-circle fa-lg text-success",invalid:"fa fa-times-circle fa-lg",validating:"fa fa-refresh"},fields:{username:{message:"The user name is not valid",validators:{notEmpty:{message:"The user name is required."}}},email:{validators:{notEmpty:{message:"The email address is required and can't be empty"},emailAddress:{message:"The input is not a valid email address"}}},firstName:{validators:{notEmpty:{message:"The first name is required and cannot be empty"},regexp:{regexp:/^[A-Z\s]+$/i,message:"The first name can only consist of alphabetical characters and spaces"}}},lastName:{validators:{notEmpty:{message:"The last name is required and cannot be empty"},regexp:{regexp:/^[A-Z\s]+$/i,message:"The last name can only consist of alphabetical characters and spaces"}}},phoneNumber:{validators:{notEmpty:{message:"The phone number is required and cannot be empty"},digits:{message:"The value can contain only digits"}}},address:{validators:{notEmpty:{message:"The address is required"}}}}}).on("success.field.bv",function(a,b){var c=b.element.parents(".form-group");c.removeClass("has-success")}).on("error.form.bv",function(b){a=!1})});