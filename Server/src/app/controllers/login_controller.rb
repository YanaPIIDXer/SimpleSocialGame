class LoginController < ApplicationController
    def index
        name = params[:name]
        user = User.find_by(name: name)
        if user == nil then
            user = User.new(name: name)
            if !user.save then
                render json: {message: "fail."}, status: 304
                return
            end
        end

        session[:id] = user.id
        render json: {message: "success.", stone: user.stone}
    end
end
