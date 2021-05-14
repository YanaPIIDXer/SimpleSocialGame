class LoginController < ApplicationController
    def index
        name = params[:name]
        user = User.find_by(name: name)
        if user == nil then
            user = User.new(name: name)
            if !user.save then
                render json: {message: "Create User Failed."}, status: 304
            end
        end
        render json: {message: "Create User Success."}
    end
end
