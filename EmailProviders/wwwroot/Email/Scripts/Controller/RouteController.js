var mailCtrl = function ($scope) {
    $scope.Test = "Mail Service Providers";
}

var sendCtrl = function ($scope, APIServiceFactory) {
    $scope.Test = "Mail Drafting";
    
    $scope.SendEmail = function () {
        $scope.ReturnValue = "";
        if (
            ( $scope.to != undefined && $scope.subject != undefined && $scope.text != undefined) &&
            ($scope.text && $scope.subject && $scope.to )) {
        emailObject = {
            "from": $scope.from,
            "to": $scope.to,
            "cc": $scope.cc,
            "bcc": $scope.bcc,
            "subject": $scope.subject,
            "text": $scope.text          
        }
        if (emailObject != null) {
            var result = APIServiceFactory.getAPICallsWithModel("Email/SendEmail", emailObject)
                .then(function (response) {
                    console.log("Data received at the client" + response.data);
                    if (response.status == 200) {
                        $scope.ReturnValue = response.data;                        
                        

                    }
                    else {
                        $scope.ReturnValue = response.data;  
                    }
                });

        }
    }
    else {
        alert("Enter atleast To recipient, subject  and body section ");
        return;
    }
}
}

