Rails.application.routes.draw do
  root 'health_check#index'
  post '/login' => "login#index"
  get '/expansions' => "expansions#index"
  post '/expansions/draw' => "expansions#draw"
  # For details on the DSL available within this file, see http://guides.rubyonrails.org/routing.html
end
